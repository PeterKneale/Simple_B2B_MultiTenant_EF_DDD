namespace Simple.Web.Pages.Auth;

public class LoginModel
{
    [Display(Name = "Email")]
    [Required]
    [BindProperty]
    [StringLength(MaximumEmailLength)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; init; }

    [Display(Name = "Password")]
    [Required]
    [BindProperty]
    [DataType(DataType.Password)]
    [MinLength(MinimumPasswordLength)]
    [MaxLength(MaximumPasswordLength)]
    public string Password { get; init; }
}