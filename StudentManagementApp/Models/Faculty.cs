using System.ComponentModel.DataAnnotations;

namespace StudentManagementApp.Models;

public class Faculty
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}