using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Simple.App.Users.Queries;

namespace Simple.Web.Pages.Auth;

public class AuthenticationService(ISender module, IHttpContextAccessor accessor, ILogger<AuthenticationService> logs)
{
    public async Task<bool> AuthenticateAsTenant(string email, string password)
    {
        var result = await module.Send(new CanAuthenticate.Query(email, password));
        if (result.Success == false)
        {
            logs.LogWarning("Authentication was not successful: {Email}", email);
            return false;
        }

        if (!result.UserId.HasValue || !result.TenantId.HasValue)
        {
            logs.LogWarning("Authentication was not successful, no ids returned: {Email}", email);
            return false;
        }

        var userId = result.UserId.Value;
        var tenantId = result.TenantId.Value;

        var claims = new Claim[]
        {
            new(UserIdClaim, userId.ToString()),
            new(TenantIdClaim, tenantId.ToString()),
            new(ClaimTypes.Name, email),
            new(ClaimTypes.Role, TenantRoleName)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await accessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        logs.LogInformation("Authentication was successful: {Email}", email);

        return true;
    }

    public async Task<bool> AuthenticateAsAdmin(string email, string password)
    {
        var admin_email = "admin@example.org";
        var admin_password = "admin@example.org";
        var correctEmail = email.Equals(admin_email, StringComparison.InvariantCultureIgnoreCase);
        var correctPassword = password.Equals(admin_password, StringComparison.InvariantCulture);
        if (!correctEmail || !correctPassword)
        {
            return false;
        }
        
        var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new(ClaimTypes.Name, "Admin"),
            new(ClaimTypes.Role, AdminRoleName)
        }, CookieAuthenticationDefaults.AuthenticationScheme));

        await accessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        logs.LogInformation("Site Admin Authentication was successful: {Email}", email);
        return true;
    }
}