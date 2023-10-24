using StudentManagementApp.Models;

namespace StudentManagementApp.Interfaces;

public interface IFacultyRepository : IGenericMethods<FacultyModel>
{
    Task<IEnumerable<FacultyModel>> GetWhatFacultiesAStudentAttends(int studentId);
}