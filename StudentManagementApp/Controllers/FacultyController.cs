using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Data;
using StudentManagementApp.Identity;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;
using StudentManagementApp.Repository;

namespace StudentManagementApp.Controllers;

[ApiController]
[Route("api/faculty")]
public class FacultyController : Controller
{
    private readonly IFacultyRepository _repository;

    public FacultyController(IFacultyRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("whatFacultiesAStudentAttends")]
    [Authorize]
    public async Task<IEnumerable<FacultyModel>> GetWhatFacultiesAStudentAttends()
    {
        string? authorizationHeader = Request.Headers["Authorization"];
        var id = JwtParser.ParseJwt(authorizationHeader);
        if (id == -1)
            return new List<FacultyModel>();

        return await _repository.GetWhatFacultiesAStudentAttends(id);
    }

    [HttpPost("add")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public ActionResult AddFaculty(FacultyModel faculty)
    {
        var value = _repository.Add(faculty);
        return value ? Ok($"Faculty {faculty.Name} created") : BadRequest("Faculty not added");
    }

    [HttpGet("all")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IEnumerable<FacultyModel>> Faculties()
    {
        return await _repository.AllAsync();
    }

    [HttpPut("update")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public ActionResult UpdateFaculty(FacultyModel faculty)
    {
        var value = _repository.Update(faculty);
        return value ? Ok("Faculty updated") : BadRequest("Faculty not updated");
    }

    [HttpDelete("delete")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public ActionResult DeleteFaculty(FacultyModel faculty)
    {
        try
        {
            var value = _repository.Delete(faculty);
            return value ? Ok("Faculty deleted") : BadRequest("Faculty not deleted");
        }
        catch
        {
            return BadRequest("Faculty not deleted. Please contact the database administrator");
        }
    }
}