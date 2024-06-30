using Simple.App.Users.Queries;

namespace Simple.Web.Pages.Profile;

public class IndexPage(IMediator mediator) : BasePageModel
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
}