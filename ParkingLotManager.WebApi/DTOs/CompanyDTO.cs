using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ValueObjects;

namespace ParkingLotManager.WebApi.DTOs;

public class CompanyDTO
{
    public string Name { get; set; }
    public Cnpj Cnpj { get; set; }
    public Address Address { get; set; }
    public string Telephone { get; set; }
    public int CarSlots { get; set; }
    public int MotorcycleSlots { get; set; }

    public IList<Vehicle>? Vehicles { get; set; }
    public IList<User>? Users { get; set; }

    public virtual object Display()
    {
        var companyDto = new
        {
            Name = this.Name,
            Cnpj = this.Cnpj,
            Address = this.Address,
            Telephone = this.Telephone,
            CarSlots = this.CarSlots,
            MotorcycleSlots = this.MotorcycleSlots,
        };

        return companyDto;
    }
}
