using ParkingLotManager.ReportApi.DTOs;
using ParkingLotManager.ReportApi.Models;

namespace ParkingLotManager.ReportApi.Interfaces;

public interface IVehicleQuery
{
    public Task<List<VehicleModel>> GetVehiclesAsync();
}
