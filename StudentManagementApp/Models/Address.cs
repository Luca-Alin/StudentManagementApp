using System.ComponentModel.DataAnnotations;

namespace StudentManagementApp.Models;

public class Address
{
    [Key]
    public int Id { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public short Number { get; set; }
}