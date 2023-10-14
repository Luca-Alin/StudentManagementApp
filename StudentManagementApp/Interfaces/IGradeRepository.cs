using StudentManagementApp.Models;

namespace StudentManagementApp.Interfaces;

public interface IGradeRepository : IGenericMethods<GradeModel>
{
    Task<int> GetGradeByStudentIdAndCourseId(int studentId, int courseId);
}