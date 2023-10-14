using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementApp.Models.Student;
[Table("student")]
public class StudentModel
{
    [Key] public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string PasswordHash { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public AddressModel AddressModel { get; set; } = null!;
}