using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingLotManager.WebApi.Attributes;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels;
using System.Text.Json.Serialization;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    /// <summary>
    /// Check API status
    /// </summary>
    /// <returns>API status</returns>
    /// <response code="200">Ok</response>
    /// <response code="500">API Offline</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("home/status-check")]
    public IActionResult CheckStatus()
    {
        try
        {
            return Ok(new { message = "API is online" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "00EX0000 - API offline" });
        }        
    }

    /// <summary>
    /// Validate API Key
    /// </summary>
    /// <returns></returns>
    [HttpGet("home/validate-api-key")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ValidateApiKey()
    {
        return Ok(new ResultViewModel<string>("Valid ApiKey", null));
    }
}
