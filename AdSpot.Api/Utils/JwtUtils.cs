namespace AdSpot.Api.Utils;

public static class JwtUtils
{
    public static string GenerateToken(User user, IConfiguration config)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(config["Jwt:Issuer"],
        config["Jwt:Audience"],
        null,
        expires: DateTime.Now.AddMinutes(120),
        signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

