using Microsoft.IdentityModel.Tokens;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models.Contracts;
using ParkingLotManager.WebApi.ValueObjects;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;

namespace ParkingLotManager.WebApi.Models;

public class Company : ICompany
{
    public string Name { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public Address Address { get; private set; }
    public string Telephone { get; private set; }
    public int CarSlots { get; set; }
    public int MotorcycleSlots { get; set; }

    public IList<Vehicle>? Vehicles { get; private set; }
    public IList<User>? Users { get; private set; }

    public virtual Company Create(RegisterCompanyViewModel viewModel)
    {
        Name = viewModel.Name;
        Cnpj = viewModel.Cnpj;
        Address = viewModel.Address;
        Telephone = viewModel.Telephone;
        CarSlots = viewModel.CarSlots == 0 ? 1 : viewModel.CarSlots;
        MotorcycleSlots = viewModel.MotorcycleSlots == 0 ? 1 : viewModel.MotorcycleSlots;

        return this;
    }

    public virtual Company Update(UpdateCompanyViewModel viewModel, Address address)
    {
        Name = viewModel.Name.IsNullOrEmptyOrWhiteSpace() ? this.Name : viewModel.Name;
        Cnpj = !viewModel.Cnpj.IsValid ? this.Cnpj : viewModel.Cnpj;

        if(this.Address == null )
        {
            this.Address = new Address(address.Street, address.City, address.ZipCode);
        }
        Address = viewModel.Address == null ? Address : this.Address.Update(viewModel.Address);
                
        Telephone = viewModel.Telephone.IsNullOrEmptyOrWhiteSpace() ? this.Telephone : viewModel.Telephone;
        CarSlots = viewModel.CarSlots == 0 || viewModel.CarSlots == null ? this.CarSlots : viewModel.CarSlots;
        MotorcycleSlots = viewModel.MotorcycleSlots == 0 || viewModel.MotorcycleSlots == null ? this.MotorcycleSlots : viewModel.MotorcycleSlots;
        
        return this;
    }
}
