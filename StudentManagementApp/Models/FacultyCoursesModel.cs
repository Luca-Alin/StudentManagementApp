using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementApp.Models;
[Table("facultyCourses")]
public class FacultyCoursesModel
{
    [Key] public int Id { get; set; }

    [Column("FacultyId")] public Faculty Faculty { get; set; } = null!;

    public byte Year { get; set; }
    public byte Semester { get; set; }
    public CourseModel Course1 { get; set; } = null!;
    public CourseModel Course2 { get; set; } = null!;
    public CourseModel Course3 { get; set; } = null!;
    public CourseModel Course4 { get; set; } = null!;
    public CourseModel Course5 { get; set; } = null!;
    public CourseModel Course6 { get; set; } = null!;
    public CourseModel Course7 { get; set; } = null!;
}