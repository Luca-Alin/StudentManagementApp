using StudentManagementApp.Models;

namespace StudentManagementApp.Interfaces;

public interface IFacultyCourseRepository : IGenericMethods<FacultyCoursesModel>
{
    Task<IEnumerable<FacultyCoursesModel>> GetAll();
    Task<IEnumerable<FacultyCoursesModel>> GetSliceAsync(int offset, int size);
    Task<int> GetCountAsync();
}