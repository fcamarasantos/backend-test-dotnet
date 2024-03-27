using Microsoft.AspNetCore.Mvc;
using ParkingLotManager.ReportApi.DTOs;
using ParkingLotManager.ReportApi.Models;

namespace ParkingLotManager.ReportApi.Interfaces;

public interface IVehicleFlowManagement
{
    public Task<int> CheckInFlowCalc();
    public Task<int> DepartureFlowCalc();
    public Task<int> EnteredVehiclesInTheLastHour();
    public Task<int> DeparturedVehiclesInTheLastHour();
}
