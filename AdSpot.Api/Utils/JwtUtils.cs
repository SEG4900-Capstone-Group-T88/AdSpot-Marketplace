using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AdSpot.Api.Utils;

public static class JwtUtils
{
    public static string GenerateToken(User user, IOptions<JwtOptions> jwtOptions, KeyManager keyManager)
    {
        var handler = new JsonWebTokenHandler();
        var key = new RsaSecurityKey(keyManager.RsaKey);

        var subject = new ClaimsIdentity(
            new[] { new Claim("sub", user.UserId.ToString()), new Claim("name", $"{user.FirstName} {user.LastName}"), }
        );

        var token = handler.CreateToken(
            new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Value.Issuer,
                Audience = jwtOptions.Value.Audience,
                Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Value.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256),
                Subject = subject
            }
        );

        return token;
    }
}
