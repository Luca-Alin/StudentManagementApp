using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagementApp.Data.Enums;

namespace StudentManagementApp.Models;

[Table("course")]
public class CourseModel
{
    [Key] public int Id { get; set; }
    public FacultyModel Faculty { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public CourseType CourseType { get; set; }
    public int Year { get; set; }
    public Semester Semester { get; set; }
    public int CourseHours { get; set; }
    public byte SeminarHours { get; set; }
    public byte LaboratoryHours { get; set; }
    public byte ProjectHours { get; set; }
    public byte PracticeHours { get; set; }
    public ExaminationType ExaminationType { get; set; }
    public byte Credits { get; set; }
}