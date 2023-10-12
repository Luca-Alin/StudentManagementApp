using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Models;

public class WhatFacultyAStudentAttends
{
    [Key]
    public int Id { get; set; }
    [Column(name:"StudentId")]
    public StudentModel Student { get; set; } = null!;
    [Column(name:"FacultyId")]
    public Faculty Faculty { get; set; } = null!;
}