using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;
using StudentManagementApp.Models.Admin;
using StudentManagementApp.Models.Student;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace StudentManagementApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientCascade;
    }

    public DbSet<AddressModel> Addresses { get; set; } = null!;
    public DbSet<StudentModel> Students { get; set; } = null!;
    public DbSet<FacultyModel> Faculties { get; set; } = null!;

    public DbSet<WhatFacultyAStudentAttendsModel> WhatFacultyAStudentAttends { get; set; } = null!;
    public DbSet<GradeModel> Grades { get; set; } = null!;
    public DbSet<CourseModel> Courses { get; set; } = null!;
    public DbSet<AdminModel> Admin { get; set; } = null!;
}