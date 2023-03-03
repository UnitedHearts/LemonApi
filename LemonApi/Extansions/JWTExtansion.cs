using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LemonApi.Extensions;

public static class JWTExtansion
{
    readonly static byte[] key = Encoding.ASCII.GetBytes("IWantToBeHappyButIAmIdiot");
    public static string? ValidateToken(string token)
    {
        if (token == null) return null;
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userEmail = jwtToken.Claims.First(x => x.Type == "email").Value;
            return userEmail;
        }
        catch
        {
            return null;
        }
    }
    public static string GetToken(string data)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, data) }),
            Expires = DateTime.UtcNow.AddDays(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}