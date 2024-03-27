using System.ComponentModel.DataAnnotations;

namespace ParkingLotManager.WebApi.ViewModels.UserViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage ="Type your email")]
    [EmailAddress(ErrorMessage ="Email is invalid")]
    public string Email { get; set; }

    [Required(ErrorMessage ="Type your password")]
    [MinLength(6, ErrorMessage ="Password must contain at least 6 characters")]
    public string Password { get; set; }
}
