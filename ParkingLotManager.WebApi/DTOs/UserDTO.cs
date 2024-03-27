using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.DTOs;

public class UserDTO
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Slug { get; private set; }

    public Company? Company { get; private set; }
    public string CompanyName { get; private set; }

    public IList<Role>? Roles { get; set; }

    public virtual object Display()
    {
        var userDto = new
        {
            Id = Id,
            Name = this.Name,
            Email = this.Email,
            Slug = this.Slug,
            CompanyName = this.CompanyName
        };

        return userDto;
    }

    public virtual List<object> DisplayList(List<UserDTO> list)
    {
        var result = new List<object>();

        foreach (var user in list)
        {
            var userDto = new
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Slug = user.Slug,
                CompanyName = user.CompanyName,
            };
            result.Add(userDto);
        }

        return result;
    }
}
