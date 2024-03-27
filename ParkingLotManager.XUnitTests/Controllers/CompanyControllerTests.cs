using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingLotManager.WebApi;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.Controllers;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;

namespace ParkingLotManager.XUnitTests.Controllers;

[Trait("CompanyController", "Unit")]
public class CompanyControllerTests
{
    private Mock<AppDataContext> _mockedDbContext = new();
    private string _apiKeyName = Configuration.ApiKeyName;
    private RegisterCompanyViewModel _registerCompanyViewModel = new(
        "Comp", new Cnpj("11111111111111"), new Address("street", "city", "0000000"), "8199999999", 15, 15);
    private UpdateCompanyViewModel _updateCompanyViewModel = new UpdateCompanyViewModel(
        "Comp", new Cnpj("11111111111111"), new Address("street", "city", "0000000"), "8199999999", 50, 50);

    [Fact]
    public async Task Get_ShouldReturnOkWhenGettingCompanyByName()
    {
        var mockedController = new Mock<CompanyController>();
        
        mockedController.Setup(m => m.GetAsyncByName(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new Company()));

        var expected = await mockedController.Object.GetAsyncByName("Comp", _apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task Get_ShouldReturnOkWhenGettingAllCompanies()
    {
        var mockedController = new Mock<CompanyController>();

        mockedController.Setup(m => m.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new List<Company>()));

        var expected = await mockedController.Object.GetAsync(_apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task Create_ShouldReturnOkWhenCreatingACompany()
    {
        var mockedController = new Mock<CompanyController>();

        mockedController.Setup(m => m.RegisterAsync(It.IsAny<RegisterCompanyViewModel>(), It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new Company()));

        var expected = await mockedController.Object.RegisterAsync(_registerCompanyViewModel, _apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }
    
    [Fact]
    public async Task Update_ShouldReturnOkWhenUpdatingACompany()
    {
        var mockedController = new Mock<CompanyController>();

        mockedController.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<UpdateCompanyViewModel>(), It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new Company()));

        var expected = await mockedController.Object.Update("Comp", _updateCompanyViewModel, _apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }
    
    [Fact]
    public async Task Delete_ShouldReturnOkWhenDeletingACompany()
    {
        var mockedController = new Mock<CompanyController>();

        mockedController.Setup(m => m.Delete(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new Company()));

        var expected = await mockedController.Object.Delete("Comp", _apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }


}
