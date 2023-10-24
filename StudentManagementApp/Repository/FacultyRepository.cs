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

    public async Task<IEnumerable<FacultyModel>> GetWhatFacultiesAStudentAttends(int studentId)
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

    public Task<IEnumerable<FacultyModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<FacultyModel?> GetByIdAsync(int id)
    {
        return _context.Faculties.Where(f => f.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<FacultyModel>> AllAsync()
    {
        return await _context.Faculties.ToListAsync();
    }

    public bool Add(FacultyModel faculty)
    {
        _context.Add(faculty);
        return Save();
    }

    public bool Update(FacultyModel faculty)
    {
        _context.Update(faculty);
        return Save();
    }

    public bool Delete(FacultyModel faculty)
    {
        _context.Remove(faculty);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }
}