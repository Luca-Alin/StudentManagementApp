using StudentManagementApp.Data.Enums;
using StudentManagementApp.Models;

namespace StudentManagementApp.Interfaces;

public interface ICourseRepository : IGenericMethods<CourseModel>
{
    Task<IEnumerable<CourseModel>> GetCoursesByFacultyAndYear(int facultyId, Year year);
    
}
