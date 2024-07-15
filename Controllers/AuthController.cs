using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZadanieRekrutacyjne.Data;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly string _key;

    public AuthController(ApiContext context)
    {
        _context = context;
        _key = "xM8OCWxEwFFLRrCzIipcu3kVJcvGBuiuYCjSdQhvRpE="; // Upewnij się, że klucz jest ten sam jak w Program.cs
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = _context.Contacts.SingleOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = "yourIssuer",
            Audience = "yourAudience"
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }
}

public class UserLogin
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
