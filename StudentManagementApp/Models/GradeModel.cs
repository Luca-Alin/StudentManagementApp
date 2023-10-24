using System.ComponentModel.DataAnnotations.Schema;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Models;

[Table("grade")]
public class GradeModel
{
    public int Id { get; set; }
    public int Value { get; set; }

    [Column("StudentId")] public StudentModel Student { get; set; } = null!;

    [Column("CourseId")] public CourseModel Course { get; set; } = null!;
}