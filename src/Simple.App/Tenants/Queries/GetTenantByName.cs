using Simple.Domain.Tenants;
using Simple.Domain.Tenants.Specifications;

namespace Simple.App.Tenants.Queries;

public static class GetTenantByName
{
    public record Query(string Name) : IRequest<Result>;

    public record Result(Guid TenantId, string TenantName);

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.Name).NotEmpty();
        }
    }

    public class Handler(IReadRepository<Tenant> tenants) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
        {
            var name = TenantName.Create(query.Name);
            var tenant = await tenants.SingleOrDefaultAsync(new TenantByNameSpec(name), cancellationToken);
            if (tenant == null)
            {
                PlatformException.ThrowNotFound(name);
            }

            return new Result(tenant.TenantId, tenant.TenantName);
        }
    }
}