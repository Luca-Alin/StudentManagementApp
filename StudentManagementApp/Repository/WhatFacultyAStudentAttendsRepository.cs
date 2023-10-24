using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;

namespace StudentManagementApp.Repository;

public class WhatFacultyAStudentAttendsRepository : IWhatFacultyAStudentAttendsRepository
{
    private readonly AppDbContext _context;

    public WhatFacultyAStudentAttendsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WhatFacultyAStudentAttendsModel>> AllAsync()
    {
        return await _context.WhatFacultyAStudentAttends.ToListAsync();
    }

    public async Task<WhatFacultyAStudentAttendsModel?> GetByIdAsync(int id)
    {
        var model = await _context
            .WhatFacultyAStudentAttends
            .Where(w => w.Id == id)
            .Include(w => w.Student)
            .Include(w => w.Student)
            .FirstOrDefaultAsync();
        return model ?? null;
    }

    public bool Add(WhatFacultyAStudentAttendsModel model)
    {
        _context.WhatFacultyAStudentAttends.Add(model);
        return Save();
    }

    public bool Update(WhatFacultyAStudentAttendsModel model)
    {
        _context.WhatFacultyAStudentAttends.Update(model);
        return Save();
    }

    public bool Delete(WhatFacultyAStudentAttendsModel model)
    {
        _context.WhatFacultyAStudentAttends.Remove(model);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }

    public async Task<bool> RemoveByStudentIdAsync(int studentId)
    {
        var whatFacultyAStudentAttends = _context.WhatFacultyAStudentAttends.Where(w => w.Student.Id == studentId);
        _context.RemoveRange(whatFacultyAStudentAttends);
        return Save();
    }
}