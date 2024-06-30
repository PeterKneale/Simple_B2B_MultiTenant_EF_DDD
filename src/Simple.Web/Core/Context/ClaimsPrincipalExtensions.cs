using System.Security.Claims;

namespace Simple.Web.Core.Context;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserIdClaim(this ClaimsPrincipal principal) =>
        Guid.Parse(principal.FindFirstValue(UserIdClaim)!);

    public static Guid GetTenantIdClaim(this ClaimsPrincipal principal) =>
        Guid.Parse(principal.FindFirstValue(TenantIdClaim)!);
}