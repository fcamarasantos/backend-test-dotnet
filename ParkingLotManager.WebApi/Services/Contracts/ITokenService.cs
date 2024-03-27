using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.Services.Contracts;

public interface ITokenService
{
    public string GenerateToken(User user);
}
