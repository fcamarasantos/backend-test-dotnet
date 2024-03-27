using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ParkingLotManager.ReportApi.DTOs;
using ParkingLotManager.ReportApi.Interfaces;

using ParkingLotManager.ReportApi.Models;
using ParkingLotManager.ReportApi.REST;
using System.Dynamic;
using System.Text.Json;

namespace ParkingLotManager.ReportApi.Services;

public class FlowManagementService : IVehicleFlowManagement
{
    private readonly IMapper _mapper;
    private readonly ParkingLotManagerWebApiService _parkingLotApi;

    protected FlowManagementService()
    {        
    }

    public FlowManagementService(IMapper mapper, ParkingLotManagerWebApiService parkingLotApi)
    {
        _mapper = mapper;
        _parkingLotApi = parkingLotApi;
    }

    /// <summary>
    /// Calculates the amount of checked-in vehicles
    /// </summary>
    /// <returns>Amount of checked-in vehicles</returns>
    public virtual async Task<int> CheckInFlowCalc()
    {
        var vehicles = await _parkingLotApi.GetVehiclesAsync();
        if (vehicles == null)
            return 0;

        var vehiclesDto = _mapper.Map<List<VehicleModelDto>>(vehicles);

        int enteredVehicles = 0;
        foreach (var vehicle in vehiclesDto)
        {
            if (vehicle.isActive)
                enteredVehicles++;
        }

        return enteredVehicles;
    }

    public async Task<int> DepartureFlowCalc()
    {
        var vehicles = await _parkingLotApi.GetVehiclesAsync();
        if (vehicles == null)
            throw new Exception("No vehicles were found");

        var vehicleDto = _mapper.Map<List<VehicleModelDto>>(vehicles);

        var departuredVehicles = 0;
        foreach (var vehicle in vehicleDto)
        {
            if (vehicle.isActive == false)
                departuredVehicles++;
        }

        return departuredVehicles;
    }

    public async Task<int> EnteredVehiclesInTheLastHour()
    {
        var vehicles = await _parkingLotApi.GetVehiclesAsync();
        if (vehicles == null)
            throw new Exception("No vehicles were found");

        var count = 0;
        var lastHour = DateTime.UtcNow;
        foreach (var vehicle in vehicles)
        {
            var exitHour = DateTime.UtcNow.AddHours(-1);
            if (vehicle.createdAt >= lastHour)
                count++;
            if (vehicle.lastUpdateDate >= exitHour || vehicle.isActive == true)
                count++;
        }

        return count;
    }

    public async Task<int> DeparturedVehiclesInTheLastHour()
    {
        var vehicles = await _parkingLotApi.GetVehiclesAsync();
        if (vehicles == null)
            throw new Exception("No vehicles were found");

        var vehiclesDepartured = 0;
        var lastHour = DateTime.UtcNow.AddHours(-1);
        foreach (var vehicle in vehicles)
        {
            if (vehicle.isActive == false && vehicle.lastUpdateDate >= lastHour)
                vehiclesDepartured++;
        }

        return vehiclesDepartured;
    }
}
