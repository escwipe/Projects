using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetInformatika.Views.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "This field is required!")]
    [EmailAddress(ErrorMessage = "Invalid email address!")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "This field is required!")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "This field is required!")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = null!;
}
