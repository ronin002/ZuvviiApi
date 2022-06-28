using ZuvviiAPI.Dtos;
using ZuvviiAPI.Models;

using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace ZuvviiAPI.Services
{
    public class TokenService
    {
        public static string GenerateToken(User user)
        {

            //var json = JsonSerializer.Serialize(roles);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Globals._KeyT);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
               
                Expires = DateTime.UtcNow.AddHours(8),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
