namespace StudentManagementApp.Interfaces;

public interface IGenericMethods<T>
{
    Task<IEnumerable<T>> All();
    bool Add(T model);
    bool Update(T model);
    bool Delete(T model);
    bool Save();
}