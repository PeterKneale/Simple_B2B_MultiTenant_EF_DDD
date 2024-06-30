using Simple.App.Users.Commands;
using Simple.App.Users.Queries;

namespace Simple.Web.Pages.Profile;

public class UpdateNamePage(IMediator mediator) : BasePageModel
{
    [BindProperty] public UpdateNameModel Model { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await mediator.Send(new GetUserById.Query(CurrentUserId));
        Model = new UpdateNameModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName
        };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await mediator.Send(new UpdateName.Command(CurrentUserId, Model.FirstName, Model.LastName));
            AlertSuccess("Your name has been changed");
            return RedirectToPage(nameof(Index));
        }
        catch (PlatformException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return Page();
        }
    }
}