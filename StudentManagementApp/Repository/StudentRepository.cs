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

    public async Task<IEnumerable<StudentModel>> GetAll()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<IEnumerable<StudentModel>> GetSliceAsync(int offset, int size)
    {
        return await _context.Students
            .Include(s => s.AddressModel)
            .Skip(offset)
            .Take(size)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return _context.Students.Count();
    }

    public Task<IEnumerable<StudentModel>> All()
    {
        throw new NotImplementedException();
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

    public async Task<StudentModel> GetByIdAsync(int id)
    {
        var student = await _context.Students
            .Include(s => s.AddressModel)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (student == null)
            throw new Exception("student not found");
        
        return student;
    }
}