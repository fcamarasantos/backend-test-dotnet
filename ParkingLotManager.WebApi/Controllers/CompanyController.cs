using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Attributes;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.DTOs;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System.Data.Common;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
[ApiKey]
public class CompanyController : ControllerBase
{
    private readonly AppDataContext _ctx;
    private readonly IMapper _mapper;
    private const string apiKeyName = Configuration.ApiKeyName;

    protected CompanyController()
    {        
    }

    public CompanyController(AppDataContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }  


    /// <summary>
    /// Get collection of registered companies
    /// </summary>
    /// <returns>registered companies data</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("v1/companies/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetAsync([FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var companies = await _ctx.Companies.AsNoTracking().ToListAsync();
            if (companies == null)
                return BadRequest(new { message = "05EX5000 - Request could not be processed. Please try another time" });

            var companiesDto = _mapper.Map<List<CompanyDTO>>(companies);

            return Ok(new JsonResult(companiesDto));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5001 - Internal server error"));
        }
    }

    /// <summary>
    /// Get company by name
    /// </summary>
    /// <param name="name">company name</param>
    /// <param name="apiKeyName">API Key</param>
    /// <returns>company data</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("v1/companies/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetAsyncByName(
        [FromRoute] string name,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var company = await _ctx.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
            if (company == null)
                return NotFound(new { message = "05EX5002 - Company not found." });

            var companyDto = _mapper.Map<CompanyDTO>(company).Display();

            return Ok(new JsonResult(companyDto));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Company>("05EX5003 - Internal server error"));
        }
    }

    /// <summary>
    /// Register a new company
    /// </summary>
    /// <remarks>
    /// {"name":"string","cnpj":{"cnpjNumber":"string"},"address":{"street":"string","city":"string","zipCode":"string"},"telephone":"string","carSlots":0,"motorcycleSlots":0}
    /// </remarks>
    /// <param name="viewModel">company ViewModel</param>
    /// <param name="apiKeyName">API Key</param>
    /// <returns>data from the new company</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost("v1/companies")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterCompanyViewModel viewModel,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<RegisterCompanyViewModel>(ModelState.GetErrors()));
        try
        {
            var company = new Company();
            company.Create(viewModel);
            var companyDto = _mapper.Map<CompanyDTO>(company).Display();

            await _ctx.Companies.AddAsync(company);
            await _ctx.SaveChangesAsync();

            return Created($"v1/companies/{company.Name}", new JsonResult(companyDto));
        }
        catch (DbException)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5002 - Could not register company"));

        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5003 - Internal server error"));
        }
    }

    /// <summary>
    /// Update a company
    /// </summary>
    /// <remarks>
    /// {"name":"string","cnpj":{"cnpjNumber":"string"},"address":{"street":"string","city":"string","zipCode":"string"},"telephone":"string","carSlots":0,"motorcycleSlots":0}
    /// </remarks>
    /// <param name="name">company name</param>
    /// <param name="viewModel">company UpdateViewModel</param>
    /// <param name="apiKeyName">API key</param>
    /// <returns>company and its updated data</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("v1/companies/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> Update(
        [FromRoute] string name,
        [FromBody] UpdateCompanyViewModel viewModel,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<UpdateCompanyViewModel>(ModelState.GetErrors()));
        
        try
        {
            var company = await _ctx.Companies.FirstOrDefaultAsync(x => x.Name == name);
            if (company == null)
                return NotFound(new ResultViewModel<UpdateCompanyViewModel>("05EX5007 - Company not found"));
            
            company.Update(viewModel, viewModel.Address);
            var companyDto = _mapper.Map<CompanyDTO>(company).Display();

            _ctx.Update(company);
            await _ctx.SaveChangesAsync();

            return Ok(new JsonResult(companyDto));
        }
        catch (DbException)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5007 - Could not update company"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5008 - Internal server error"));
        }
    }

    /// <summary>
    /// Delete a company
    /// </summary>
    /// <param name="name">company name</param>
    /// <param name="apiKeyName">API key</param>
    /// <returns>deleted company</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("v1/companies/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> Delete(
        [FromRoute] string name,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var company = await _ctx.Companies.FirstOrDefaultAsync(x => x.Name == name);
            if(company == null)
                return BadRequest(new ResultViewModel<Company>("05EX5005 - Company not found"));

            var companyDto = _mapper.Map<CompanyDTO>(company).Display();

            _ctx.Remove(company);
            await _ctx.SaveChangesAsync();

            return Ok(new JsonResult(companyDto));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5006 - Internal server error"));
        }
    }
}
