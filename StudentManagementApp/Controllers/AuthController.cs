using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementApp.Data;
using StudentManagementApp.Models.Admin;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration, AppDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }


    [HttpPost("student/login")]
    public ActionResult<string> Login(StudentDto request)
    {
        var student = _context.Students.FirstOrDefault(s => s.Email == request.Email);

        if (student == null)
            return BadRequest("User not found.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, student.PasswordHash))
            return BadRequest("User not found");

        var jwt = CreateJwtToken(
            new List<Claim>
            {
                new Claim("StudentId", student.Id.ToString()),
                new Claim(ClaimTypes.Email, student.Email)
            });

        return Ok(jwt);
    }

    [HttpPost("admin/login")]
    public ActionResult<string> AdminLogin(AdminDto request)
    {
        var admin = _context.Admin.FirstOrDefault(a => a.Email == request.Email);
        if (admin == null)
            return BadRequest("Admin does not exist");

        var token = CreateJwtToken(
            new List<Claim>
            {
                new Claim("admin", "true"),
                new Claim(ClaimTypes.Email, admin.Email)
            });

        return Ok(token);
    }

    private string CreateJwtToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!
        ));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}