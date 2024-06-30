using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple.App.Tenants.Commands;

namespace Simple.Web.Pages.Auth;

public class RegisterPage(ISender module, ILogger<RegisterPage> logger) : BasePageModel
{
    [BindProperty] public RegisterModel Model { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var tenantId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var command = new Register.Command(tenantId, Model.TenantName, userId, Model.FirstName, Model.LastName, Model.Email, Model.Password);
            await module.Send(command);

            AlertSuccess("You have been successfully registered.");
            return Redirect("/");
        }
        catch (PlatformException e)
        {
            logger.LogWarning("Registration was not successful: {Message}", e.Message);
            AlertDanger("Registration was not successful:.");
            ModelState.AddModelError(string.Empty, e.Message);
            return Page();
        }
    }
}