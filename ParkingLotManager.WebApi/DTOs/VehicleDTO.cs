using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.DTOs;

public class VehicleDTO
{
    public string LicensePlate { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public string Color { get; private set; }
    public EVehicleType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public bool IsActive { get; private set; }

    public Company? Company { get; private set; }
    public string CompanyName { get; private set; }

    public virtual object Display()
    {
        var vehicleDto = new
        {
            licensePlate = this.LicensePlate,
            brand = this.Brand,
            model = this.Model,
            color = this.Color,
            Type = this.Type,
            IsActive = this.IsActive,
            CompanyName = this.CompanyName
        };
        
        return vehicleDto;
    }

    public virtual List<object> DisplayList(List<VehicleDTO> list)
    {
        var result = new List<object>();
        
        foreach(var user in list)
        {
            var vehicleDto = new
            {
                licensePlate = this.LicensePlate,
                brand = this.Brand,
                model = this.Model,
                color = this.Color,
                Type = this.Type,
                CompanyName = this.CompanyName
            };
            result.Add(vehicleDto);
        }

        return result;
    }
}
