using Microsoft.IdentityModel.Tokens;
using ParkingLotManager.WebApi.DTOs;
using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models.Contracts;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;

namespace ParkingLotManager.WebApi.Models;

public class Vehicle : IVehicle
{
    public string LicensePlate { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public EVehicleType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public bool IsActive { get; set; }

    public Company? Company { get; set; }
    public string CompanyName { get; set; }

    public virtual Vehicle Create(RegisterVehicleViewModel viewModel)
    {
        LicensePlate = viewModel.LicensePlate;
        Model = viewModel.Model;
        Brand = viewModel.Brand;
        Color = viewModel.Color;
        Type = viewModel.Type;
        CompanyName = viewModel.CompanyName;
        IsActive = true;
        
        return this;
    }
    
    public virtual Vehicle Update(UpdateVehicleViewModel viewModel)
    {
        LicensePlate = viewModel.LicensePlate.IsNullOrEmptyOrWhiteSpace() ? LicensePlate : viewModel.LicensePlate;
        Brand = viewModel.Brand.IsNullOrEmptyOrWhiteSpace() ? Brand : viewModel.Brand;
        Model = viewModel.Model.IsNullOrEmptyOrWhiteSpace() ? Model : viewModel.Model;
        Color = viewModel.Color.IsNullOrEmptyOrWhiteSpace() ? Color : viewModel.Color;
        Type = viewModel.Type != Type ? viewModel.Type : Type;
        CompanyName = viewModel.CompanyName.IsNullOrEmptyOrWhiteSpace() ? CompanyName : viewModel.CompanyName;

        return this;
    }

    public virtual bool Departure()
    {
        this.IsActive = false;
        this.LastUpdateDate = DateTime.UtcNow;
        return true;
    }

    public virtual bool Reentered()
    {
        this.IsActive = true;
        this.LastUpdateDate = DateTime.UtcNow;
        return true;
    }

    static void ChangeLicensePlate(string licensePlate, Vehicle vehicle)
    {
        if (licensePlate.IsNullOrEmptyOrWhiteSpace())
            return;
        vehicle.LicensePlate = licensePlate;
    }
}
