using Moq;
using ParkingLotManager.WebApi.Controllers;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingLotManager.XUnitTests.Entities;

[Trait("Entities", "Unit")]
public class UserTests
{
    private User _user = new();
    private Mock<User> _mockedUser = new();
    private readonly CreateUserViewModel _createUserViewModel = new CreateUserViewModel("superman", "superman@io.com", "parkingManagement");
    private readonly UpdateUserViewModel _updateUserViewModel = new UpdateUserViewModel("batman@io.com", "Batman", "abc123");
    private readonly string _password = "123456";

    [Fact]
    public void Create_ShouldCreateAUser()
    {
        // 1. Arrange phase
        _mockedUser.Setup(m => m.Create(It.IsAny<CreateUserViewModel>(), _password))
            .Returns(_user);

        // 2. Act phase
        var actual = _user.Create(_createUserViewModel, _password);
        var expected = _mockedUser.Object.Create(_createUserViewModel, _password);
        
        // 3. Assert phase
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Update_ShouldUpdateAUser()
    {
        //1
        _mockedUser.Setup(m => m.Update(It.IsAny<UpdateUserViewModel>()))
            .Returns(_user);

        //2
        var actual = _user.Update(_updateUserViewModel);
        var expected = _mockedUser.Object.Update(_updateUserViewModel);

        //3
        Assert.Equal(expected, actual);
    }
}
