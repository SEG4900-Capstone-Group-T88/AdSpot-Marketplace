namespace AdSpot.Api.Utils;

public static class JwtUtils
{
    public static string GenerateToken(User user, IOptions<JwtOptions> jwtOptions)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            jwtOptions.Value.Issuer,
            jwtOptions.Value.Audience,
            null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

