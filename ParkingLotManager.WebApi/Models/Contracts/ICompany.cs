using ParkingLotManager.WebApi.ValueObjects;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;

namespace ParkingLotManager.WebApi.Models.Contracts;

public interface ICompany
{
    public Company Create(RegisterCompanyViewModel viewModel);

    public Company Update(UpdateCompanyViewModel viewModel, Address address);
}
