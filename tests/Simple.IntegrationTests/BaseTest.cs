using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Simple.Domain.Tenants;
using Simple.Domain.Users;

namespace Simple.IntegrationTests;

[Collection(nameof(ServiceFixtureCollection))]
public abstract class BaseTest
{
    private readonly ServiceFixture _service;
    private readonly ITestOutputHelper _output;

    protected BaseTest(ServiceFixture service, ITestOutputHelper output)
    {
        _service = service;
        _output = output;
        _service.OutputHelper = _output;
    }

    protected void Log(object o)
    {
        _output.WriteLine(JsonConvert.SerializeObject(o, Formatting.Indented));
    }
    
    protected void Log(string message)
    {
        _output.WriteLine(message);
    }

    protected async Task Command(IRequest command)
    {
        await using var scope = _service.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(command);
    }

    protected async Task<TResponse> Query<TResponse>(IRequest<TResponse> query)
    {
        await using var scope = _service.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(query);
    }
    
    protected async Task Command(IRequest command, FakeTenant tenant, FakeUser user)
    {
        await using var scope = GetContextualScope(tenant.TenantId, user.UserId);
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(command);
    }

    protected async Task<TResponse> Query<TResponse>(IRequest<TResponse> query, FakeTenant tenant, FakeUser user)
    {
        await using var scope = GetContextualScope(tenant.TenantId, user.UserId);
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(query);
    }

    private AsyncServiceScope GetContextualScope(Guid tenantId, Guid userId)
    {
        var scope = _service.ServiceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<IExecutionContext>();
        context.Set(tenantId, userId);
        return scope;
    }
}