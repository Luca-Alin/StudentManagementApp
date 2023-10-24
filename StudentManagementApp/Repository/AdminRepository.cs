using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models.Admin;

namespace StudentManagementApp.Repository;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AdminModel>> AllAsync()
    {
        return await _context.Admin.ToListAsync();
    }

    public Task<AdminModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public bool Add(AdminModel model)
    {
        _context.Add(model);
        return Save();
    }

    public bool Update(AdminModel model)
    {
        _context.Update(model);
        return Save();
    }

    public bool Delete(AdminModel model)
    {
        _context.Remove(model);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }

    public bool AdminExist(AdminDto request)
    {
        var admin = _context.Admin.FirstOrDefault(a => a.Email == request.Email);
        if (admin == null) return false;

        return BCrypt.Net.BCrypt.Verify(request.Password, admin.PasswordHash);
    }
}