using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;

namespace StudentManagementApp.Repository;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseModel>> AllAsync()
    {
        return await _context.Courses.Include(c => c.Faculty)
            .ToListAsync();
    }

    public async Task<CourseModel?> GetByIdAsync(int id)
    {
        var model = await _context
            .Courses
            .Where(w => w.Id == id)
            .Include(c => c.Faculty)
            .FirstOrDefaultAsync();
        return model ?? null;
    }

    public bool Add(CourseModel model)
    {
        _context.Courses.Add(model);
        return Save();
    }

    public bool Update(CourseModel model)
    {
        _context.Courses.Update(model);
        return Save();
    }

    public bool Delete(CourseModel model)
    {
        _context.Courses.Remove(model);
        return Save();
    }

    public async Task<IEnumerable<CourseModel>> GetCoursesByFacultyAndYear(int facultyId, int year)
    {
        return await _context.Courses.Where(c => c.Faculty.Id == facultyId && c.Year == year)
            .Include(c => c.Faculty)
            .ToListAsync();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }
}