using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models.Contracts;
using ParkingLotManager.WebApi.ViewModels.UserViewModels;
using SecureIdentity.Password;
using System.Xml;

namespace ParkingLotManager.WebApi.Models;

public class User : IUser
{
    public int Id { get; set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }    
    public string Slug { get; private set; }

    public Company? Company { get; private set; }
    public string CompanyName { get; private set; }

    public IList<Role>? Roles { get; set; }

    public virtual User Create(CreateUserViewModel viewModel, string password)
    {
        Name = viewModel.Name;
        Email = viewModel.Email;
        PasswordHash = PasswordHasher.Hash(password);
        Slug = viewModel.Email.Replace("@", "-").Replace(".", "-");
        CompanyName = viewModel.CompanyName;

        return this;
    }

    public virtual User Update(UpdateUserViewModel viewModel)
    {
        Name = viewModel.Name.IsNullOrEmptyOrWhiteSpace() ? Name : viewModel.Name;
        Email = viewModel.Email.IsNullOrEmptyOrWhiteSpace() ? Email : viewModel.Email;
        PasswordHash = viewModel.PasswordHash.IsNullOrEmptyOrWhiteSpace() ? PasswordHash : viewModel.PasswordHash;

        return this;
    }

    public virtual User CreateAdmin(CreateUserViewModel viewModel, string password)
    {
        Name = viewModel.Name;
        Email = viewModel.Email;
        PasswordHash = PasswordHasher.Hash(password);
        Roles = new List<Role> { new Role() { Name = "admin", Slug = "admin" } };
        Slug = viewModel.Email.Replace("@", "-").Replace(".", "-");
        CompanyName = viewModel.CompanyName;

        return this;
    }
}