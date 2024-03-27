using Moq;
using ParkingLotManager.WebApi;
using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.Models.Contracts;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using ParkingLotManager.WebApi.Controllers;
using ParkingLotManager.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ParkingLotManager.XUnitTests.Entities;

public sealed class VehicleTests
{
    private Vehicle _vehicle = new();
    private Mock<Vehicle> _mockedVehicle = new Mock<Vehicle>();
    private Mock<AppDataContext> _mockedDbContext = new();
    public readonly RegisterVehicleViewModel _registerVehicleViewModel = new RegisterVehicleViewModel("AAA0000", "Ferrari", "f-50", "red", EVehicleType.Car, "WellPark Inc");
    private readonly UpdateVehicleViewModel _updateVehicleViewModel = new UpdateVehicleViewModel("AAA0000", "Ferrari", "f-50", "red", EVehicleType.Motorcycle, "WellPark Inc");

    [Fact]
    public void Create_ShouldCreateAVehicle()
    {
        //1. Arrange
        _mockedVehicle.Setup(x => x.Create(It.IsAny<RegisterVehicleViewModel>()))
            .Returns(_vehicle);

        //2. Act
        var actual = _vehicle.Create(_registerVehicleViewModel);
        var expected = _mockedVehicle.Object.Create(_registerVehicleViewModel);
        
        //3. Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Update_ShouldUpdateAVehicle()
    {
        //1
        var dbVehicle = new Vehicle();
        _mockedVehicle.Setup(m => m.Update(It.IsAny<UpdateVehicleViewModel>())).Returns(dbVehicle);

        //2
        var actual = dbVehicle.Update(_updateVehicleViewModel);
        var expected =  _mockedVehicle.Object.Update(_updateVehicleViewModel);

        //3
        Assert.Equal(expected, actual);
    }
}
