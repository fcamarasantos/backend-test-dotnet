using ParkingLotManager.WebApi.ValueObjects;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System.ComponentModel.DataAnnotations;

namespace ParkingLotManager.WebApi.ViewModels.CompanyViewModels;

public class UpdateCompanyViewModel
{
    public UpdateCompanyViewModel()
    {        
    }

    public UpdateCompanyViewModel(string? name, Cnpj? cnpj, Address? address, string? telephone, int carSlots, int motorcycleSlots)
    {
        Name = name;
        Cnpj = cnpj;
        Address = address;
        Telephone = telephone;
        CarSlots = carSlots == 0 ? CarSlots : carSlots;
        MotorcycleSlots = motorcycleSlots == 0 ? MotorcycleSlots : motorcycleSlots;
    }

    public string? Name { get; set; }
    public Cnpj? Cnpj { get; set; }
    public Address? Address { get; set; }
    public string? Telephone { get; set; }
    public int CarSlots { get; set; }
    public int MotorcycleSlots { get; set; }

    public bool CheckIfAllEmpty(UpdateVehicleViewModel viewModel)
    {
        Type type = viewModel.GetType();
        var props = type.GetProperties();
        var count = 0;

        foreach (var prop in props)
        {
            var value = prop.GetValue(viewModel);
            if (value == "" || prop.PropertyType.IsValueType && value.Equals(Activator.CreateInstance(prop.PropertyType)))
                count++;
        }

        return count == props.Length;
    }
}
