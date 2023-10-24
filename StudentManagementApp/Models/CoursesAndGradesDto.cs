namespace StudentManagementApp.Models;

public class CoursesAndGradesDto
{
    public List<KeyValuePair<CourseModel?, GradeModel?>>? CoursesAndGrades { get; set; } = new();
}