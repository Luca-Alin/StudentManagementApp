using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Data.Enums;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;

namespace StudentManagementApp.Controllers;

[ApiController]
public class CourseController : Controller
{
    private readonly ICourseRepository _repository;

    public CourseController(ICourseRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("api/course/all")]
    public async Task<IEnumerable<CourseModel>> GetAllCourses()
    {
        return await _repository.All();
    }
    [HttpGet("api/course/{facultyId}/{year}")]
    public async Task<IEnumerable<CourseModel>> GetCoursesByFacultyAndYear(int facultyId, Year year)
    {
        return await _repository.GetCoursesByFacultyAndYear(facultyId, year);
    }
}