using Moq;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;
using ParkingLotManager.WebApi.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingLotManager.XUnitTests.Entities;

[Trait("CompanyEntity", "Unit")]
public class CompanyTests
{
    private Company _company = new();
    private Mock<Company> _mockedCompany = new();
    private readonly Address _address =  new("street", "city", "000000");
    private readonly RegisterCompanyViewModel _registerCompanyViewModel = new RegisterCompanyViewModel(
        "name", new Cnpj("00000000000000"), new Address("street", "city", "000000"), "659999999", 10, 10);
    private readonly UpdateCompanyViewModel _updateCompanyViewModel = new UpdateCompanyViewModel(
        "comp", new Cnpj("11111111111111"), new Address("street2", "city2", "1111111"), "3388888888", 15, 15);

    [Fact]
    public void Create_ShouldCreateACompany()
    {
        _mockedCompany.Setup(m => m.Create(It.IsAny<RegisterCompanyViewModel>()))
            .Returns(_company);

        var actual = _company.Create(_registerCompanyViewModel);
        var expected = _mockedCompany.Object.Create(_registerCompanyViewModel);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Update_ShouldUpdateACompany()
    {
        _mockedCompany.Setup(m => m.Update(It.IsAny<UpdateCompanyViewModel>(), It.IsAny<Address>()))
            .Returns(_company);

        var actual = _company.Update(_updateCompanyViewModel, _address);
        var expected = _mockedCompany.Object.Update(_updateCompanyViewModel, _address);

        Assert.Equal(expected, actual);
    }   
}
