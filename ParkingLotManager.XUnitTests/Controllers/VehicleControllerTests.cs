using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ParkingLotManager.WebApi;
using ParkingLotManager.WebApi.Controllers;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingLotManager.XUnitTests.Controllers;

[Trait("VehicleController", "Unit")]
public class VehicleControllerTests
{
    private Mock<AppDataContext> _mockedContext = new();
    private readonly string apiKeyName = Configuration.ApiKeyName;
    private readonly RegisterVehicleViewModel _registerVehicleViewModel = new RegisterVehicleViewModel(
        "AAA0000", "GM", "F-50", "Black", EVehicleType.Car, "Park4you");
    private readonly UpdateVehicleViewModel _updateVehicleViewModel = new(
        "AAA0000", "GM", "F-1000", "Red", EVehicleType.Car, "Park4you");

    [InlineData("AAA0000")]
    [Theory]
    public async Task Get_ShouldGetVehicleByLicensePlateAsync(string licensePlate)
    {
        var dbVehicle = new Vehicle();
        var _mockedController = new Mock<VehicleController>(_mockedContext.Object);
       
        _mockedController.Setup(m => m.GetByLicensePlateAsync(It.IsAny<string>(), Configuration.ApiKeyName))
            .ReturnsAsync(new OkObjectResult(dbVehicle));

        var expected = await _mockedController.Object.GetByLicensePlateAsync(licensePlate, apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task GetAll_ShouldGetAllVehiclesAsync()
    {
        var _mockedController = new Mock<VehicleController>(_mockedContext.Object);
        
        _mockedController.Setup(m => m.GetAsync(Configuration.ApiKeyName))
            .ReturnsAsync(new OkObjectResult(new List<Vehicle>()));

        var expected = await _mockedController.Object.GetAsync(apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task Create_ShouldRegisterAVehicle()
    {
        var createdVehicle = new Vehicle();
        var _mockedController = new Mock<VehicleController>(_mockedContext.Object);
        
        _mockedController.Setup(m => m.RegisterAsync(It.IsAny<RegisterVehicleViewModel>(), apiKeyName))
            .ReturnsAsync(new OkObjectResult(createdVehicle));

        var expected = await _mockedController.Object.RegisterAsync(_registerVehicleViewModel, apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task Update_ShouldUpdateAVehicle()
    {
        var createdVehicle = new Vehicle();
        var updatedVehicle = createdVehicle.Update(_updateVehicleViewModel);
        var _mockedController = new Mock<VehicleController>(_mockedContext.Object);
        
        _mockedController.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<UpdateVehicleViewModel>(), apiKeyName))
            .ReturnsAsync(new OkObjectResult(updatedVehicle));

        var expected = await _mockedController.Object.Update("", _updateVehicleViewModel, apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task Delete_ShouldDeleteAVehicle()
    {
        var createdVehicle = new Vehicle();
        var _mockedController = new Mock<VehicleController>(_mockedContext.Object);

        _mockedController.Setup(m => m.Delete(It.IsAny<string>(), apiKeyName))
            .ReturnsAsync(new OkObjectResult(""));

        var expected = await _mockedController.Object.Delete("AAA0000", apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }
}