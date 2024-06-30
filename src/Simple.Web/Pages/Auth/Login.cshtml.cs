using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Simple.Web.Pages.Auth;

public class LoginPage(AuthenticationService authentication) : BasePageModel
{
    [BindProperty] public LoginModel Model { get; set; }

    public async Task<IActionResult> OnPostAsync(string? returnUrl)
    {
        if (!ModelState.IsValid) return Page();

        try
        {
            var result = await authentication.AuthenticateWithCredentials(Model.Email, Model.Password);
            if (!result)
            {
                AlertDanger("Login failed. Please try again.");
                return Page();
            }

            AlertSuccess("You have been successfully logged in.");
            return Redirect(returnUrl ?? "/");
        }
        catch (PlatformException e)
        {
            ModelState.AddModelError(string.Empty, e.Message!);
            return Page();
        }
    }
}