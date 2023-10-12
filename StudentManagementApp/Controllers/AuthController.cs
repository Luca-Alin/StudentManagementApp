using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementApp.Models;

namespace StudentManagementApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;
    private new static readonly User User = new();

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("register")]
    public ActionResult<User> Register(UserDto request)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        User.UserName = request.UserName;
        User.PasswordHash = passwordHash;

        
        return Ok(User);
    }

    [HttpPost("login")]
    public ActionResult<User> Login(UserDto request)
    {
        if (User.UserName != request.UserName)
            return BadRequest("User not found.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, User.PasswordHash))
            return BadRequest("Wrong password.");

        string token = CreateToken(User);
        return Ok(token);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!
        ));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}