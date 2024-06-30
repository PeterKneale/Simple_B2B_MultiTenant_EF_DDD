using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Simple.App.Tenants.Queries;
using Simple.App.Users.Queries;
using Simple.Domain.Tenants;
using Simple.Domain.Users;

namespace Simple.Web.Core.Context;

public class SetContextMiddleware(ISender sender, ILogger<SetContextMiddleware> log) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var identity = context.User.Identity!;
        if (!identity.IsAuthenticated)
        {
            log.LogDebug("Not setting context, user is not authenticated");
            await next(context);
            return;
        }
        
        if (context.Request.Path == "/auth/logout")
        {
            log.LogDebug("Not setting context, user is logging out");
            await next(context);
            return;
        }

        try
        {
            var tenantId = context.User.GetTenantIdClaim();
            var userId = context.User.GetUserIdClaim();

            var tenant = await sender.Send(new GetTenantById.Query(tenantId));
            var user = await sender.Send(new GetUserById.Query(userId));
        
            log.LogDebug("Setting context: User {User} and Tenant {Tenant}", user.UserName, tenant.TenantName);

            context.SetUser(new UserContext(user.UserId, user.UserName));
            context.SetTenant(new TenantContext(tenant.TenantId, tenant.TenantName));
        }
        catch (PlatformException e)
        {
            log.LogError(e, "Cannot set context, forcing logout");
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            context.Response.Redirect("/");
        }
        
        await next(context);
    }
}