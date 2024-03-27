using System.ComponentModel.DataAnnotations;

namespace ParkingLotManager.WebApi.ViewModels.UserViewModels;

public class CreateUserViewModel
{
    public CreateUserViewModel(string name, string email, string companyName)
    {
        Name = name;
        Email = email;
        CompanyName = companyName;
    }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Company name is required")]
    public string CompanyName { get; set; } = string.Empty;
}