using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;
using StudentManagementApp.Repository;

namespace StudentManagementApp.Controllers;

[ApiController, Authorize] 
public class FacultyController : Controller
{
    private readonly IFacultyRepository _repository;
    public FacultyController(IFacultyRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("api/faculty/studentFaculties")]
    public async Task<IEnumerable<Faculty>> GetWhatFacultiesAStudentAttends()
    {
        string? authorizationHeader = Request.Headers["Authorization"];
        int id = JwtParser.ParseJwt(authorizationHeader);
        if (id == -1)
            return new List<Faculty>();
        
        return await _repository.GetWhatFacultiesAStudentAttends(id);
    }
    

}