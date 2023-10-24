using StudentManagementApp.Models;

namespace StudentManagementApp.Interfaces;

public interface IAddressRepository : IGenericMethods<AddressModel>
{
    Task<bool> RemoveByIdAsync(int id);
}