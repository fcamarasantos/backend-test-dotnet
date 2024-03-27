using ParkingLotManager.WebApi.Models;
using System.Collections;
using System.Security.Claims;

namespace ParkingLotManager.WebApi.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
        };

        result.AddRange(
                user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug))
            );

        return result;
    }
}
