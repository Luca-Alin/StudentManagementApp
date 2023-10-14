using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Controllers;

[ApiController, Authorize]
public class StudentController : Controller
{
    private readonly IStudentRepository _repository;

    public StudentController(IStudentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("/all")]
    public Task<IEnumerable<StudentModel>> Students()
    {
        return _repository.GetAll();
    }
    
    [HttpGet("api/students/student")]
    public Task<StudentModel> Student()
    {
        string? authorizationHeader = Request.Headers["Authorization"];
        int id = JwtParser.ParseJwt(authorizationHeader);
        if (id == -1)
            return Task.FromResult(new StudentModel());
        
        return _repository.GetByIdAsync(id);
    }
    
}