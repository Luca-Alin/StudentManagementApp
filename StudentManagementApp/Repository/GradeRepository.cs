using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;

namespace StudentManagementApp.Repository;

public class GradeRepository : IGradeRepository
{
    private readonly AppDbContext _context;
    public GradeRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<GradeModel>> AllAsync()
    {
        throw new NotImplementedException();
    }

    public bool Add(GradeModel model)
    {
        _context.Grades.Add(model);
        return Save();
    }

    public bool Update(GradeModel model)
    {
        _context.Grades.Update(model);
        return Save();
    }

    public bool Delete(GradeModel model)
    {
        _context.Grades.Remove(model);
        return Save();
    }

    public async Task<GradeModel?> GetGradeByStudentIdAndCourseId(int studentId, int courseId)
    {
        var grade = await _context.Grades
            .Where(g => g.Student.Id == studentId && g.Course.Id == courseId)
            .FirstOrDefaultAsync();

        return grade;
    }

    public async Task<bool> DeleteByStudentId(int studentId)
    {
        var grades = await _context.Grades.Where(g => g.Student.Id == studentId).ToListAsync();
        _context.RemoveRange(grades!);
        
        return Save();
    }

    public async Task<GradeModel?> GetByIdAsync(int id)
    {
        var grade = await _context.Grades.Where(g => g.Id == id).FirstOrDefaultAsync();
        return grade;
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }
}