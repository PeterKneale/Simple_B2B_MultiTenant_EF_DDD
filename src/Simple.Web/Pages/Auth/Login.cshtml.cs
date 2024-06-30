namespace Simple.Web.Pages.Auth;

public class LoginPage(AuthenticationService authentication) : BasePageModel
{
    [BindProperty] public LoginModel Model { get; set; }

    public async Task<IActionResult> OnPostAsync(string? returnUrl)
    {
        if (!ModelState.IsValid) return Page();

        try
        {
            var admin = await authentication.AuthenticateAsAdmin(Model.Email, Model.Password);
            if (admin)
            {
                AlertSuccess("You have been successfully logged in.");
                return Redirect(returnUrl ?? "/Admin/Index");
            }
            var result = await authentication.AuthenticateAsTenant(Model.Email, Model.Password);
            if (!result)
            {
                AlertDanger("Login failed. Please try again.");
                return Page();
            }

            AlertSuccess("You have been successfully logged in.");
            return Redirect(returnUrl ?? "/Tenant/Index");
        }
        catch (PlatformException e)
        {
            ModelState.AddModelError(string.Empty, e.Message!);
            return Page();
        }
    }
}