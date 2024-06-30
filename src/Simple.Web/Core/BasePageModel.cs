using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Simple.Web.Core;

public abstract class BasePageModel : PageModel
{
    protected Guid CurrentUserId => HttpContext.User.GetUserIdClaim();

    protected void AlertSuccess(string message)
    {
        TempData.SetAlert(Alert.Success(message));    
    }
    
    protected void AlertDanger(string message)
    {
        TempData.SetAlert(Alert.Danger(message));    
    }
}