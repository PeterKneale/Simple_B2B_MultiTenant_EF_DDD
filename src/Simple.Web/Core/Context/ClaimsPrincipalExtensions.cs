using System.Security.Claims;

namespace Simple.Web.Core.Context;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserIdClaim(this ClaimsPrincipal principal) =>
        Guid.Parse(principal.FindFirstValue(UserIdClaim)!);

    public static Guid GetTenantIdClaim(this ClaimsPrincipal principal) =>
        Guid.Parse(principal.FindFirstValue(TenantIdClaim)!);
    
    public static bool IsTenant(this ClaimsPrincipal principal) =>
        principal.IsInRole(TenantRoleName);
    
    public static bool IsAdmin(this ClaimsPrincipal principal) =>
        principal.IsInRole(AdminRoleName);
    
    public static string GetRoleClaim(this ClaimsPrincipal principal) =>
        principal.FindFirstValue(ClaimTypes.Role)!;
}