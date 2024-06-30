using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Simple.App.Users.Queries;

namespace Simple.Web.Pages.Auth;

public class AuthenticationService(ISender module, IHttpContextAccessor accessor, ILogger<AuthenticationService> logs)
{
    public async Task<bool> AuthenticateWithCredentials(string email, string password)
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
            new(ClaimTypes.Name, email)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await accessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        logs.LogInformation("Authentication was successful: {Email}", email);

        return true;
    }
}