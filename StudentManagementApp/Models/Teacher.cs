using System.ComponentModel.DataAnnotations;

namespace StudentManagementApp.Models;

public class Teacher
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Address Address { get; set; } = null!;
}