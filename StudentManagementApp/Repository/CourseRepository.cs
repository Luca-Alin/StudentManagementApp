using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Data.Enums;
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

    public async Task<IEnumerable<CourseModel>> All()
    {
        return await _context.Courses.Include(c => c.Faculty).ToListAsync();
    }

    public bool Add(CourseModel model)
    {
        return Save();
    }

    public bool Update(CourseModel model)
    {
        return Save();
    }

    public bool Delete(CourseModel model)
    {
        return Save();
    }

    public async Task<IEnumerable<CourseModel>> GetCoursesByFacultyAndYear(int facultyId, Year year)
    {
        return await _context.Courses.Where(c => c.Faculty.Id == facultyId && c.Year == year).Include(c => c.Faculty).ToListAsync();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }
}