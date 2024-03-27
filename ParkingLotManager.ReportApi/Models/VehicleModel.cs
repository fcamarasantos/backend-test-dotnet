using AutoMapper.Configuration.Annotations;
using Newtonsoft.Json;
using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models;
using System.Text.Json.Serialization;

namespace ParkingLotManager.ReportApi.Models;

public class VehicleModel
{
    public string licensePlate { get; set; }
    public string brand { get; set; }
    public string model { get; set; }
    public string color { get; set; }
    public int type { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime lastUpdateDate { get; set; }
    public bool isActive { get; set; }
    public object company { get; set; }
    public string companyName { get; set; }
}
