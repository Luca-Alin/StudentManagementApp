using System.Data.Entity;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;

namespace StudentManagementApp.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AddressModel>> AllAsync()
    {
        return await _context.Addresses.ToListAsync();
    }

    public Task<AddressModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public bool Add(AddressModel model)
    {
        _context.Addresses.Add(model);
        return Save();
    }

    public bool Update(AddressModel model)
    {
        _context.Addresses.Update(model);
        return Save();
    }

    public bool Delete(AddressModel model)
    {
        _context.Addresses.Remove(model);
        return Save();
    }

    public bool Save()
    {
        var changes = _context.SaveChanges();
        return changes > 0;
    }

    public async Task<bool> RemoveByIdAsync(int id)
    {
        var address = await _context.Addresses.Where(a => a.Id == id).FirstOrDefaultAsync();
        return Delete(address);
    }
}