using Microsoft.AspNetCore.Mvc;
using ParkingLotManager.ReportApi.REST;
using ParkingLotManager.ReportApi.Services;

namespace ParkingLotManager.ReportApi.Controller;

[ApiController]
public class ReportController : ControllerBase
{
    private readonly FlowManagementService _flowManagementService;

    public ReportController(FlowManagementService flowManagementService)
        => _flowManagementService = flowManagementService;

    [HttpGet("v1/report/entered-vehicles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEnteredVehicles()
    {
        var enteredVehicles = await _flowManagementService.CheckInFlowCalc();

        return new JsonResult(new { message = $"There are {enteredVehicles} vehicles in the parking lot" });
    }

    [HttpGet("v1/report/departured-vehicles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeparturedVehicles()
    {
        var departuredVehicles = await _flowManagementService.DepartureFlowCalc();

        return new JsonResult(new { message = $"{departuredVehicles} vehicles have already departured" });
    }

    [HttpGet("v1/report/entered-vehiclesLH")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEnteredVehiclesLastHour()
    {
        var vehiclesLastHour = await _flowManagementService.EnteredVehiclesInTheLastHour();

        return new JsonResult(new { message = $"{vehiclesLastHour} vehicles entered in the last hour" });
    }

    [HttpGet("v1/report/departured-vehiclesLH")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeparturedVehiclesLastHour()
    {
        var vehiclesLastHour = await _flowManagementService.DeparturedVehiclesInTheLastHour();

        return new JsonResult(new { message = $"{vehiclesLastHour} vehicles departured in the last hour" });
    }
}
