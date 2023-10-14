using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Models;
[Table("whatFacultyAStudentAttends")]
public class WhatFacultyAStudentAttendsModel
{
    [Key] public int Id { get; set; }

    [Column("StudentId")] public StudentModel Student { get; set; } = null!;

    [Column("FacultyId")] public Faculty Faculty { get; set; } = null!;
}