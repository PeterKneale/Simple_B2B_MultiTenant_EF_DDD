namespace Simple.Web.Core.Context;

internal static class HttpContextExtensions
{
    private const string TenantContextKey = nameof(TenantContextKey);
    private const string UserContextKey = nameof(UserContextKey);

    public static void SetTenant(this HttpContext context, TenantContext name) => 
        context.Items.Add(TenantContextKey, name);
    
    public static void SetUser(this HttpContext context, UserContext name) => 
        context.Items.Add(UserContextKey, name);

    public static TenantContext GetTenant(this HttpContext context) =>
        context.Items.TryGetValue(TenantContextKey, out var item)
            ? (TenantContext)item!
            : throw new InvalidOperationException($"{TenantContextKey} not available in http context");
    
    public static UserContext GetUser(this HttpContext context)=>
        context.Items.TryGetValue(UserContextKey, out var item)
            ? (UserContext)item!
            : throw new InvalidOperationException($"{UserContextKey} not available in http context");
}

internal record TenantContext(Guid TenantId, string TenantName);

internal record UserContext(Guid UserId, string UserName);