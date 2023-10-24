using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Controllers;

[ApiController]
[Route("api/coursesAndGrades")]
public class CoursesAndGradesController : Controller
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IGradeRepository _gradeRepository;

    public CoursesAndGradesController(IFacultyRepository facultyRepository, ICourseRepository courseRepository,
        IGradeRepository gradeRepository)
    {
        _facultyRepository = facultyRepository;
        _courseRepository = courseRepository;
        _gradeRepository = gradeRepository;
    }

    [HttpGet("{facultyId}/{year}")]
    [Authorize]
    public async Task<CoursesAndGradesDto> GetCoursesAndGrades(int facultyId, int year)
    {
        string? authorizationHeader = Request.Headers["Authorization"];
        var studentId = JwtParser.ParseJwt(authorizationHeader);
        if (studentId == -1)
            return new CoursesAndGradesDto();

        var cagd = new CoursesAndGradesDto();

        var courses = await _courseRepository.GetCoursesByFacultyAndYear(facultyId, year);
        foreach (var c in courses)
        {
            var grade = await _gradeRepository.GetGradeByStudentIdAndCourseId(studentId, c.Id);
            cagd.CoursesAndGrades?.Add(new KeyValuePair<CourseModel?, GradeModel?>(
                c,
                grade
            ));
        }

        return cagd;
    }
}