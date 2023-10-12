using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<StudentModel> Students { get; set; } = null!;
    public DbSet<Faculty> Faculties { get; set; } = null!;
    public DbSet<WhatFacultyAStudentAttends> WhatFacultyAStudentAttends { get; set; } = null!;
    // public DbSet<Grade> Grades { get; set; } = null!;
    // public DbSet<Course> Courses { get; set; } = null!;
    // public DbSet<Teacher> Teachers { get; set; } = null!;
    // public DbSet<FacultyCourses> FacultyCourses { get; set; } = null!;
}