using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Data;
using StudentManagementApp.Identity;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Controllers;

[ApiController]
[Route("api/grade")]
public class GradeController : Controller
{
    private readonly IGradeRepository _gradeRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;

    public GradeController(IGradeRepository gradeRepository, IStudentRepository studentRepository,
        ICourseRepository courseRepository)
    {
        _gradeRepository = gradeRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
    }

    [HttpGet("studentGrade/{courseId}")]
    [Authorize]
    public async Task<GradeModel> GetGradeByCourseId(int courseId)
    {
        string? authorizationHeader = Request.Headers["Authorization"];
        var id = JwtParser.ParseJwt(authorizationHeader);
        return (await _gradeRepository.GetGradeByStudentIdAndCourseId(id, courseId))!;
    }

    [HttpGet("{courseId}/{studentId}")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<ActionResult> GetGradeByStudentIdAndCourseId(int studentId, int courseId)
    {
        var grade = await _gradeRepository.GetGradeByStudentIdAndCourseId(studentId, courseId);
        if (grade == null)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (student == null || course == null) return BadRequest("Student or course not found");

            grade = new GradeModel
            {
                Student = student,
                Course = course,
                Value = 0
            };

            _gradeRepository.Add(grade);
        }

        return Ok(grade);
    }

    [HttpPut("update/{courseId}/{studentId}/{value}")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<ActionResult> UpdateGradeByCourseIdAndStudentId(int courseId, int studentId, int value)
    {
        var grade = await _gradeRepository.GetGradeByStudentIdAndCourseId(studentId, courseId);
        if (grade == null)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (student == null || course == null) return BadRequest("Student or course not found");

            grade = new GradeModel
            {
                Student = student,
                Course = course,
                Value = 0
            };

            _gradeRepository.Add(grade);
        }

        grade.Value = value;
        var a = _gradeRepository.Update(grade);

        return a ? Ok("Grade updated") : BadRequest("Grade not updated");
    }
}