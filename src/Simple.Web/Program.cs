using Microsoft.AspNetCore.Authentication.Cookies;
using Simple.Infra;
using Simple.Web.Pages.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToPage("/Auth/Forbidden");
    options.Conventions.AllowAnonymousToPage("/Auth/ForgotPassword");
    options.Conventions.AllowAnonymousToPage("/Auth/ResetPassword");
    options.Conventions.AllowAnonymousToPage("/Auth/Login");
    options.Conventions.AllowAnonymousToPage("/Auth/Logout");
    options.Conventions.AllowAnonymousToPage("/Auth/Register");
});
builder.Services.AddApplication();
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SetContextMiddleware>();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
        options.SlidingExpiration = true;
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/Forbidden";
    });
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SetContextMiddleware>();

app.MapRazorPages();

app.Run();
