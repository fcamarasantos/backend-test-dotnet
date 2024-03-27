using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ParkingLotManager.WebApi.ViewModels.UserViewModels;

public class UpdateUserViewModel
{
    public UpdateUserViewModel()
    {        
    }

    public UpdateUserViewModel(string? email, string? name, string? passwordHash)
    {
        Email = email;
        Name = name;
        PasswordHash = passwordHash;
    }

    [JsonPropertyName("Email")]
    [EmailAddress]
    public string? Email { get; set; }
    [JsonPropertyName("Name")]
    public string? Name { get; set; }
    [JsonPropertyName("PasswordHash")]
    public string? PasswordHash { get; set; }
}
