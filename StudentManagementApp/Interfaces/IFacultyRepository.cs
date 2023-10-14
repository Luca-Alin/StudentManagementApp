using StudentManagementApp.Models;

namespace StudentManagementApp.Interfaces;

public interface IFacultyRepository : IGenericMethods<Faculty>
{
    Task<IEnumerable<Faculty>> GetWhatFacultiesAStudentAttends(int studentId);
    Task<IEnumerable<Faculty>> GetAll();
}