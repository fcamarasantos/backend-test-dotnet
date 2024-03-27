using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingLotManager.WebApi;
using ParkingLotManager.WebApi.Controllers;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;
using ParkingLotManager.WebApi.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingLotManager.XUnitTests.Controllers;

[Trait("AccountController", "Unit")]
public class AccountControllerTests
{
    private readonly string _apiKeyName = Configuration.ApiKeyName;
    private Mock<AppDataContext> _mockedDbContext = new();
    private CreateUserViewModel _createUserViewModel = new(
        "test", "test", "compTest");
    private UpdateUserViewModel _updateUserViewModel = new(
        "test", "test", "pass12345");

    [Fact(DisplayName ="Return User by Id")]
    public async Task Get_ShouldReturnOkWhenGettingUserById()
    {
        var mockedController = new Mock<AccountController>();

        mockedController.Setup(m => m.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new User()));

        var expected = await mockedController.Object.GetByIdAsync(1, _apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task Get_ShouldReturnOkWhenGettingAllUsers()
    {
        var mockedController = new Mock<AccountController>();

        mockedController.Setup(m => m.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new List<User>()));

        var expected = await mockedController.Object.GetAsync(_apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task Create_ShouldReturnOkWhenCreatingAUser()
    {
        var mockedController = new Mock<AccountController>();

        mockedController.Setup(m => m.CreateAsync(It.IsAny<CreateUserViewModel>(), It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new User()));

        var expected = await mockedController.Object.CreateAsync(_createUserViewModel, _apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task Update_ShouldReturnOkWhenUpdatingAUser()
    {
        var mockedController = new Mock<AccountController>();

        mockedController.Setup(m => m.Update(It.IsAny<int>(), It.IsAny<UpdateUserViewModel>(), It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new User()));

        var expected = await mockedController.Object.Update(1, _updateUserViewModel, _apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async Task Delete_ShouldReturnOkWhenDeletingACompany()
    {
        var mockedController = new Mock<AccountController>();

        mockedController.Setup(m => m.Delete(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(new OkObjectResult(new Company()));

        var expected = await mockedController.Object.Delete(1, _apiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }
}
