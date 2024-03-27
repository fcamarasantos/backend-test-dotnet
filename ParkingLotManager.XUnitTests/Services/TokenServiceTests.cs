using Moq;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingLotManager.XUnitTests.Services;

public class TokenServiceTests
{
    [Fact]
    public void Should_return_token_if_User_is_given()
    {
        var mockedService = new Mock<TokenService>();
        var user = new User();

        mockedService.Setup(x => x.GenerateToken(It.IsAny<User>()))
            .Returns("fixed_string");

        var mockedToken = mockedService.Object.GenerateToken(user);
        Assert.Equal("fixed_string", mockedToken);
    }
}
