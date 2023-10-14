using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Interfaces;

public interface IStudentRepository : IGenericMethods<StudentModel>
{
    Task<IEnumerable<StudentModel>> GetAll();
    Task<StudentModel> GetByIdAsync(int id);
    Task<IEnumerable<StudentModel>> GetSliceAsync(int offset, int size);
    Task<int> GetCountAsync();
}