using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementApp.Models;

[Table("address")]
public class AddressModel
{
    [Key] public int Id { get; set; }

    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public short Number { get; set; }
}