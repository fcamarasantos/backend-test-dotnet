using ParkingLotManager.WebApi.ViewModels.UserViewModels;

namespace ParkingLotManager.WebApi.Models.Contracts;

public interface IUser
{
    public User Create(CreateUserViewModel viewModel, string password);

    public User Update(UpdateUserViewModel viewModel);

    public User CreateAdmin(CreateUserViewModel viewModel, string password);
}
