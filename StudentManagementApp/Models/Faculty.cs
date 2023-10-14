using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementApp.Models;
[Table("faculty")]
public class Faculty
{
    [Key] public int Id { get; set; }

    public string Name { get; set; } = null!;
}