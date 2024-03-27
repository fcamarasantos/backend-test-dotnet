using Moq;
using ParkingLotManager.ReportApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingLotManager.XUnitTests.Services;

public class FlowManagementServiceTests
{
    private readonly Mock<FlowManagementService> _mockedService = new();

    [Fact]
    public void Should_return_amount_of_entered_vehicles()
    {
        _mockedService.Setup(x => x.CheckInFlowCalc())
            .ReturnsAsync(1);

        var result = _mockedService.Object.CheckInFlowCalc();
        Assert.IsType<Task<int>>(result);
    }
}
