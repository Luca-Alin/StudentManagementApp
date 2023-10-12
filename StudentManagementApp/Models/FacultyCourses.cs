using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementApp.Models;

public class FacultyCourses
{
    [Key]
    public int Id { get; set; }
    [Column(name:"FacultyId")]
    public Faculty Faculty { get; set; } = null!;
    public byte Year { get; set; }
    public byte Semester { get; set; }
    public Course Course1 { get; set; } = null!;
    public Course Course2 { get; set; } = null!;
    public Course Course3 { get; set; } = null!;
    public Course Course4 { get; set; } = null!;
    public Course Course5 { get; set; } = null!;
    public Course Course6 { get; set; } = null!;
    public Course Course7 { get; set; } = null!;
}