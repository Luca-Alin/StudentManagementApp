using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;

namespace StudentManagementApp.Repository;

public class FacultyRepository : IFacultyRepository
{
    private readonly AppDbContext _context;

    public FacultyRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Faculty>> GetWhatFacultiesAStudentAttends(int studentId)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
        var facultiesIds =
            _context
                .WhatFacultyAStudentAttends
                .Where(f => f.Student == student)
                .Include(whatFacultyAStudentAttendsModel =>
                    whatFacultyAStudentAttendsModel.Faculty)
                .ToList()
                .Select(f => f.Faculty);

        var faculties = _context.Faculties.Where(f => facultiesIds.Contains(f));

        return await faculties.ToListAsync();
    }

    public Task<IEnumerable<Faculty>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Faculty>> All()
    {
        throw new NotImplementedException();
    }

    public bool Add(Faculty studentModel)
    {
        throw new NotImplementedException();
    }

    public bool Update(Faculty studentModel)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Faculty studentModel)
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        throw new NotImplementedException();
    }
}