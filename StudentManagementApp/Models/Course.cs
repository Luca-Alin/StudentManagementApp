using System.ComponentModel.DataAnnotations;
using StudentManagementApp.Data.Enums;

namespace StudentManagementApp.Models;

public class Course
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ExaminationType ExaminationType { get; set; }
    public byte Credits { get; set; }
    public byte NumberOfHours { get; set; }
}