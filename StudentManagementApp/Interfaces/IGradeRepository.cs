using StudentManagementApp.Models;

namespace StudentManagementApp.Interfaces;

public interface IGradeRepository : IGenericMethods<GradeModel>
{
    Task<GradeModel?> GetGradeByStudentIdAndCourseId(int studentId, int courseId);
    Task<bool> DeleteByStudentId(int studentId);
}