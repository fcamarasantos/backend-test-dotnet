using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Attributes;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.DTOs;
using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System.Data.Common;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class VehicleController : ControllerBase
{
    private readonly AppDataContext _ctx;
    private readonly IMapper _mapper;
    private const string apiKeyName = Configuration.ApiKeyName;

    protected VehicleController()
    {
    }

    public VehicleController(AppDataContext ctx, IMapper mapper)
    {
        _mapper = mapper;
        _ctx = ctx;
    }

    /// <summary>
    /// Get collection of vehicles
    /// </summary>
    /// <returns>collection of vehicles</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("v1/vehicles")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetAsync([FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var vehicles = await _ctx.Vehicles.AsNoTracking().ToListAsync();
            if (vehicles == null)
                return BadRequest(new { message = "01EX1000 - Request could not be processed. Please try another time" });

            var resultList = vehicles.VehiclesToDtoList(vehicles, _mapper);

            return new JsonResult(resultList);
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Vehicle>>("01EX1001 - Internal server error"));
        }
    }

    /// <summary>
    /// Get vehicle by licensePlate
    /// </summary>
    /// <returns>vehicle data by licensePlate</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("v1/vehicles/{licensePlate}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetByLicensePlateAsync(
        [FromRoute] string licensePlate,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var vehicle = await _ctx.Vehicles.AsNoTracking().FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle is null)
                return NotFound(new { message = "01EX1002 - License plate not found." });

            var vehicleMapping = _mapper.Map<VehicleDTO>(vehicle);
            var vehicleDto = vehicleMapping.Display();

            return Ok(new ResultViewModel<dynamic>(vehicleDto));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX1003 - Internal server error"));
        }
    }

    /// <summary>
    /// Get collection of Ford vehicles. Only works with Admin privileges
    /// </summary>
    /// <returns>collection of Ford vehicles</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("v1/vehicles/ford")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetIfBrandIsFordAsync()
    {
        try
        {
            var fordVehicles = await _ctx.Vehicles.Where(x => x.Brand == "Ford").AsNoTracking().ToListAsync();
            if (fordVehicles == null)
                return NotFound(new ResultViewModel<string>("01EX1004 - Request could not be processed. Please try again another time"));

            var resultList = fordVehicles.VehiclesToDtoList(fordVehicles, _mapper);

            return new JsonResult(resultList);
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX1005 - Internal server error"));
        }
    }

    /// <summary>
    /// Register a new vehicle
    /// </summary>
    /// <remarks>
    /// {"company":{"cnpj":{"cnpjNumber":"string"},"address":{"street":"string","city":"string","zipCode":"string"}},"licensePlate":"strings","brand":"string","model":"string","color":"string","type":1,"companyName":"string"}
    /// </remarks>
    /// <returns>new vehicle data</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost("v1/vehicles")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterVehicleViewModel viewModel,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<RegisterVehicleViewModel>(ModelState.GetErrors()));
        try
        {
            var company = await _ctx.Companies.FirstOrDefaultAsync(x => x.Name == viewModel.CompanyName);
            if (company == null)
                return new JsonResult(new { message = "Company does not exist" });

            if (viewModel.Type == EVehicleType.Car)
            {
                if (company.CarSlots == 0)
                    return new JsonResult(new { message = "Sorry, car slots are full" });
            }
            if (viewModel.Type == EVehicleType.Motorcycle)
            {
                if (company.MotorcycleSlots == 0)
                    return new JsonResult(new { message = "Sorry, motorcycle slots are full" });
            }

            var createdVehicle = new Vehicle().Create(viewModel);
            var vehicleDto = _mapper.Map<VehicleDTO>(createdVehicle).Display();

            await _ctx.Vehicles.AddAsync(createdVehicle);
            await _ctx.SaveChangesAsync();

            return Created($"vehicles/v1/{createdVehicle.LicensePlate}", new JsonResult(vehicleDto));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX2000 - Could not register vehicle"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX2001 - Internal server error"));
        }

    }

    /// <summary>
    /// Update data of a registered vehicle
    /// </summary>
    /// <returns>updated data of vehicle</returns>
    /// <remarks>
    /// {"company":{"cnpj":{"cnpjNumber":"string"},"address":{"street":"string","city":"string","zipCode":"string"}},"licensePlate":"strings","brand":"string","model":"string","color":"string","type":1,"companyName":"string"}
    /// </remarks>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("v1/vehicles/{licensePlate}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> Update(
        [FromRoute] string licensePlate,
        [FromBody] UpdateVehicleViewModel viewModel,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Vehicle>(ModelState.GetErrors()));
        try
        {
            var vehicle = await _ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle == null)
                return NotFound(new ResultViewModel<Vehicle>("01EX3000 - Vehicle not found."));

            vehicle.Update(viewModel);
            var updatedDto = _mapper.Map<VehicleDTO>(vehicle).Display();

            _ctx.Update(vehicle);
            await _ctx.SaveChangesAsync();

            return new JsonResult(updatedDto);
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX3001 - Internal server error"));
        }
    }

    /// <summary>
    /// Delete vehicle by licensePlate
    /// </summary>
    /// <returns>data of deleted vehicle</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("v1/vehicles/{licensePlate}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> Delete(
        [FromRoute] string licensePlate,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var vehicle = await _ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle == null)
                return NotFound(new ResultViewModel<Vehicle>("01EX4000 - Vehicle not found."));

            var deletedDto = _mapper.Map<VehicleDTO>(vehicle).Display();

            _ctx.Vehicles.Remove(vehicle);
            await _ctx.SaveChangesAsync();

            return Ok(new JsonResult(deletedDto));
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "01EX4001 - Internal server error" });
        }
    }

    /// <summary>
    /// Registers a vehicle departure
    /// </summary>
    /// <param name="licensePlate">vehicle license plate</param>
    /// <param name="apiKeyName">API key</param>
    /// <returns>vehicle data</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("v1/vehicles/departure/{licensePlate}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> DepartureAsync(
        [FromRoute] string licensePlate,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var vehicle = await _ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle == null)
                return NotFound(new ResultViewModel<Vehicle>("01EX4001 - Vehicle not found."));

            vehicle.Departure();
            _ctx.Update(vehicle);
            await _ctx.SaveChangesAsync();

            return Ok(new JsonResult(new { message = "Vehicle has exited" }));
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "01EX4003 - Internal server error" });
        }
    }

    /// <summary>
    /// Reactivate a vehicle which already has been in the parking lot
    /// </summary>
    /// <param name="licensePlate">vehicle license plate</param>
    /// <param name="apiKeyName">API key</param>
    /// <returns>reactivation confirmation</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("v1/vehicles/reentered/{licensePlate}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> ReenteredAsync(
        [FromRoute] string licensePlate,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        var vehicle = _ctx.Vehicles.FirstOrDefault(x => x.LicensePlate == licensePlate);
        if (vehicle == null)
            return NotFound(new ResultViewModel<Vehicle>("01EX4004 - Vehicle not found."));

        vehicle.Reentered();
        _ctx.Update(vehicle);
        await _ctx.SaveChangesAsync();

        return new JsonResult(new { isActive = vehicle.IsActive, lastUpdate = vehicle.LastUpdateDate });
    }
}
