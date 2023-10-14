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

    public Task<IEnumerable<GradeModel>> All()
    {
        throw new NotImplementedException();
    }

    public bool Add(GradeModel model)
    {
        _context.Grades.Remove(model);
        return Save();
    }

    public bool Update(GradeModel model)
    {
        _context.Grades.Remove(model);
        return Save();        
    }

    public bool Delete(GradeModel model)
    {
        _context.Grades.Remove(model);
        return Save();
    }

    public async Task<int> GetGradeByStudentIdAndCourseId(int studentId, int courseId)
    {
        var grade = await _context.Grades
            .Where(g => g.Student.Id == studentId && g.CourseModel.Id == courseId)
            .FirstOrDefaultAsync();
        return grade?.Value ?? 0;
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }
}