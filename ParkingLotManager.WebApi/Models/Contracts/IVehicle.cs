using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;

namespace ParkingLotManager.WebApi.Models.Contracts;

public interface IVehicle
{
    public Vehicle Create(RegisterVehicleViewModel viewModel);

    public Vehicle Update(UpdateVehicleViewModel viewModel);
}