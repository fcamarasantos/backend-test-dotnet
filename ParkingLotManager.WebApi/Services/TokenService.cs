using Microsoft.IdentityModel.Tokens;
using ParkingLotManager.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Services.Contracts;

namespace ParkingLotManager.WebApi.Services;

public class TokenService : ITokenService
{
    public virtual string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var claims = user.GetClaims();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };        
        
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
