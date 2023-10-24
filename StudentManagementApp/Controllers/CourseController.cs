using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Identity;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;

namespace StudentManagementApp.Controllers;

[ApiController]
[Route("api/course")]
public class CourseController : Controller
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly ICourseRepository _repository;

    public CourseController(ICourseRepository repository, IFacultyRepository facultyRepository)
    {
        _repository = repository;
        _facultyRepository = facultyRepository;
    }

    [HttpGet("all")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IEnumerable<CourseModel>> GetAllCourses()
    {
        return await _repository.AllAsync();
    }

    [HttpGet("{facultyId}/{year}")]
    [Authorize]
    public async Task<IEnumerable<CourseModel>> GetCoursesByFacultyAndYear(int facultyId, int year)
    {
        return await _repository.GetCoursesByFacultyAndYear(facultyId, year);
    }

    [HttpPost("add")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<ActionResult> AddCourse(CourseModel model)
    {
        var faculty = await _facultyRepository.GetByIdAsync(model.Faculty.Id);
        if (faculty == null)
            return BadRequest("Faculty not found");

        var newCourse = new CourseModel
        {
            Code = model.Code,
            Name = model.Name,
            Year = model.Year,
            Faculty = faculty,
            Credits = model.Credits,
            Semester = model.Semester,
            CourseHours = model.CourseHours,
            CourseType = model.CourseType,
            ExaminationType = model.ExaminationType,
            LaboratoryHours = model.LaboratoryHours,
            PracticeHours = model.PracticeHours,
            ProjectHours = model.ProjectHours,
            SeminarHours = model.SeminarHours
        };

        var ok = _repository.Add(newCourse);
        return ok ? Ok($"Course {model.Name} added.") : BadRequest("Course not added");
    }

    [HttpPut("update")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public ActionResult UpdateCourse(CourseModel model)
    {
        var ok = _repository.Update(model);
        return ok ? Ok($"Course {model.Name} updated.") : BadRequest("Course not updated");
    }

    [HttpDelete("delete")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public ActionResult DeleteCourse(CourseModel model)
    {
        var ok = _repository.Delete(model);
        return ok ? Ok($"Course {model.Name} deleted.") : BadRequest("Course not deleted");
    }
}