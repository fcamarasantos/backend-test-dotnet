using Microsoft.IdentityModel.Tokens;
using ParkingLotManager.WebApi.Extensions;

namespace ParkingLotManager.WebApi.ValueObjects;

public class Address : ValueObject
{
    private Address()
    { }

    public Address(string street, string city, string zipCode)
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string ZipCode { get; private set; }

    public virtual Address Update(Address newAddress)
    {
        Street = newAddress.Street.IsNullOrEmptyOrWhiteSpace() ? Street : newAddress.Street;
        City = newAddress.City.IsNullOrEmptyOrWhiteSpace() ? City : newAddress.City;
        ZipCode = newAddress.City.IsNullOrEmptyOrWhiteSpace() ? ZipCode : newAddress.ZipCode;

        return this;
    }
}
