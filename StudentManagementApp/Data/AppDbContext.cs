using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AddressModel> Addresses { get; set; } = null!;
    public DbSet<StudentModel> Students { get; set; } = null!;
    public DbSet<Faculty> Faculties { get; set; } = null!;

    public DbSet<WhatFacultyAStudentAttendsModel> WhatFacultyAStudentAttends { get; set; } = null!;
    public DbSet<GradeModel> Grades { get; set; } = null!;
    public DbSet<CourseModel> Courses { get; set; } = null!;
    public DbSet<FacultyCoursesModel> FacultyCoursesModel { get; set; } = null!;
}