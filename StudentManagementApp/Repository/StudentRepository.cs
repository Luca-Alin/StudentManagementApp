using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentDto>> GetStudentsByFaculty(int facultyId)
    {
        var studentsInFaculty = await _context.WhatFacultyAStudentAttends
            .Where(w => w.Faculty.Id == facultyId)
            .Select(w => w.Student.Id)
            .ToListAsync();

        var students = await _context.Students
            .Where(s => studentsInFaculty.Contains(s.Id))
            .Include(s => s.Address)
            .ToListAsync();

        var studentDtos = students.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            Address = s.Address,
            DateOfBirth = s.DateOfBirth,
            PhoneNumber = s.PhoneNumber
        });

        return studentDtos;
    }

    public async Task<IEnumerable<StudentDto>> GetAll()
    {
        var students = await _context.Students.Include(s => s.Address).ToListAsync();
        var studentDtos = students.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            Address = s.Address,
            DateOfBirth = s.DateOfBirth,
            PhoneNumber = s.PhoneNumber
        });

        return studentDtos;
    }

    public async Task<IEnumerable<StudentDto>> GetSliceAsync(int offset, int size)
    {
        var students = await _context.Students
            .Include(s => s.Address)
            .Skip(offset)
            .Take(size)
            .ToListAsync();

        var studentDtos = students.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            Address = s.Address,
            DateOfBirth = s.DateOfBirth,
            PhoneNumber = s.PhoneNumber
        });

        return studentDtos;
    }

    public async Task<int> GetIdByEmailAsync(string email)
    {
        return await _context.Students.Where(s => s.Email == email).Select(s => s.Id).FirstOrDefaultAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Students.CountAsync();
    }

    public bool Add(StudentModel studentModel)
    {
        _context.Add(studentModel);
        return Save();
    }

    public bool Update(StudentModel studentModel)
    {
        _context.Update(studentModel);
        return Save();
    }

    public bool Delete(StudentModel studentModel)
    {
        _context.Remove(studentModel);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }

    public async Task<StudentModel?> GetByIdAsync(int id)
    {
        var student = await _context.Students
            .Include(s => s.Address)
            .FirstOrDefaultAsync(s => s.Id == id);


        return student;
    }

    public Task<IEnumerable<StudentModel>> AllAsync()
    {
        //Implementing this method will expose the hashed passwords from the database
        throw new NotImplementedException();
    }
}