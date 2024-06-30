using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Simple.Domain.Tenants;
using Simple.Domain.Users;

namespace Simple.IntegrationTests;

[Collection(nameof(ServiceFixtureCollection))]
public abstract class BaseTest
{
    protected BaseTest(ServiceFixture service, ITestOutputHelper output)
    {
        Service = service;
        Output = output;
        Service.OutputHelper = Output;
    }

    protected ServiceFixture Service { get; }

    protected ITestOutputHelper Output { get; }

    protected async Task Execute(IRequest command)
    {
        await using var scope = Service.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(command);
    }
    
    protected async Task Execute(IRequest command, FakeTenant tenant, FakeUser user) => 
        await Execute(command, tenant.TenantId, user.UserId);
    
    protected async Task<TResponse> Execute<TResponse>(IRequest<TResponse> query, FakeTenant tenant, FakeUser user) => 
        await Execute(query, tenant.TenantId, user.UserId);

    private async Task Execute(IRequest command, Guid tenantId, Guid userId)
    {
        await using var scope = Service.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var context = scope.ServiceProvider.GetRequiredService<IExecutionContext>();
        context.Set(tenantId, userId);
        await mediator.Send(command);
    }

    protected async Task<TResponse> Execute<TResponse>(IRequest<TResponse> query)
    {
        await using var scope = Service.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(query);
    }

    private async Task<TResponse> Execute<TResponse>(IRequest<TResponse> query, Guid tenantId, Guid userId)
    {
        await using var scope = Service.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var context = scope.ServiceProvider.GetRequiredService<IExecutionContext>();
        context.Set(tenantId, userId);
        return await mediator.Send(query);
    }

    protected void Log(object o)
    {
        Output.WriteLine(JsonConvert.SerializeObject(o, Formatting.Indented));
    }
}