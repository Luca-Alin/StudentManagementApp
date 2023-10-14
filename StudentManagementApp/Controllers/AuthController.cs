using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementApp.Data;
using StudentManagementApp.Models;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public AuthController(IConfiguration configuration, AppDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    

    [HttpPost("login")]
    public ActionResult<string> Login(StudentDto request)
    {
        var student = _context.Students.FirstOrDefault(s => s.Email == request.Email);
        
        if (student == null)
            return BadRequest("User not found.");
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, student.PasswordHash))
            return BadRequest("User not found");

        var token = CreateToken(student);
        
        return Ok(token);
    }

    private string CreateToken(StudentModel student)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Authentication, student.Id.ToString()),
            new Claim(ClaimTypes.Email, student.Email)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!
        ));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1000),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}