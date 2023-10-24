using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Interfaces;

public interface IStudentRepository : IGenericMethods<StudentModel>
{
    Task<IEnumerable<StudentDto>> GetStudentsByFaculty(int facultyId);
    Task<IEnumerable<StudentDto>> GetAll();
    Task<IEnumerable<StudentDto>> GetSliceAsync(int offset, int size);
    Task<int> GetIdByEmailAsync(string email);
    Task<int> GetCountAsync();
}