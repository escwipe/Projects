using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetInformatika.Views.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "This field is required!")]
    [EmailAddress(ErrorMessage = "Invalid email address!")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "This field is required!")]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; } = false;
}
