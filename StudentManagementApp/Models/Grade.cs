using System.ComponentModel.DataAnnotations.Schema;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Models;

public class Grade
{
    public int Id { get; set; }
    public byte Value { get; set; }
    [Column(name:"StudentId")]
    public StudentModel Student { get; set; } = null!;
    [Column(name:"CourseId")]
    public Course Course { get; set; } = null!;
}