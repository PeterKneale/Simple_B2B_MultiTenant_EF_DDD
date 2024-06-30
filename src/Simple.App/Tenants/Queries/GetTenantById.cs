using Simple.Domain.Tenants;
using Simple.Domain.Tenants.Specifications;

namespace Simple.App.Tenants.Queries;

public static class GetTenantById
{
    public record Query(Guid TenantId) : IRequest<Result>;

    public record Result(Guid TenantId, string TenantName);

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.TenantId).NotEmpty();
        }
    }

    public class Handler(IReadRepository<Tenant> tenants) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(query.TenantId);
            var tenant = await tenants.SingleOrDefaultAsync(new TenantByIdSpec(tenantId), cancellationToken);
            if (tenant == null)
            {
                PlatformException.ThrowNotFound(tenantId);
            }

            return new Result(tenant.TenantId, tenant.TenantName);
        }
    }
}