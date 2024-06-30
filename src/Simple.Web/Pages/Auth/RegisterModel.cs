namespace Simple.Web.Pages.Auth;

public class RegisterModel
{
    [Display(Name = "TenantName")]
    [Required]
    [StringLength(50)]
    public string TenantName { get; init; }

    [Display(Name = "FirstName")]
    [Required]
    [StringLength(MaxFirstNameLength)]
    public string FirstName { get; init; }

    [Display(Name = "LastName")]
    [Required]
    [StringLength(MaxLastNameLength)]
    public string LastName { get; init; }

    [Display(Name = "Email")]
    [Required]
    [StringLength(MaximumEmailLength)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; init; }

    [Display(Name = "Password")]
    [Required]
    [DataType(DataType.Password)]
    [MinLength(MinimumPasswordLength)]
    [MaxLength(MaximumPasswordLength)]
    public string Password { get; init; }
}