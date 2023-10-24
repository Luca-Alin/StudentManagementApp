namespace StudentManagementApp.Interfaces;

public interface IGenericMethods<T>
{
    Task<IEnumerable<T>> AllAsync();
    Task<T?> GetByIdAsync(int id);
    bool Add(T model);
    bool Update(T model);
    bool Delete(T model);
    bool Save();
}