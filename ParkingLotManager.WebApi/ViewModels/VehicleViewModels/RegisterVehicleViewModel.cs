using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ParkingLotManager.WebApi.ViewModels.VehicleViewModels;

public class RegisterVehicleViewModel : Vehicle
{
    public RegisterVehicleViewModel(
        string licensePlate,
        string brand,
        string model,
        string color,
        EVehicleType type,
        string companyName)
    {
        LicensePlate = licensePlate.Replace("-", "").ToUpper();
        Brand = brand;
        Model = model;
        Color = color;
        Type = type;
        CompanyName = companyName;
    }

    [Required(ErrorMessage = "License plate is required")]
    [MinLength(7, ErrorMessage = "License plate must have at least 7 characters")]
    [MaxLength(8, ErrorMessage = "License plate must not have more then 8 characters")]
    public string LicensePlate { get; private set; }

    [Required(ErrorMessage = "Brand is required")]
    public string Brand { get; private set; }

    [Required(ErrorMessage = "Model is required")]
    public string Model { get; private set; }

    [Required(ErrorMessage = "Color is required")]
    public string Color { get; private set; }

    [Required(ErrorMessage = "Type is required")]
    public EVehicleType Type { get; private set; }

    [Required(ErrorMessage = "Company name is required")]
    public string CompanyName { get; private set; }
}
