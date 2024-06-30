namespace Simple.Web.Pages.Profile;

public class UpdateNameModel
{
    [Display(Name = "First Name")]
    [Required]
    [BindProperty]
    [StringLength(MaxFirstNameLength)]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required]
    [BindProperty]
    [StringLength(MaxLastNameLength)]
    public string LastName { get; set; }
}