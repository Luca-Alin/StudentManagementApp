using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Controllers;

[ApiController]
public class GradeController : Controller
{
    private readonly IGradeRepository _gradeRepository;

    public GradeController(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }
    [HttpGet("api/grade/studentGrade/{courseId}")]
    public async Task<int> GetGradeByStudentIdAndCourseId(int courseId)
    {
        string? authorizationHeader = Request.Headers["Authorization"];
        int id = JwtParser.ParseJwt(authorizationHeader);
        return await _gradeRepository.GetGradeByStudentIdAndCourseId(id, courseId);
    }
}