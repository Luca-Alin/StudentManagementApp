using StudentManagementApp.Models;

namespace StudentManagementApp.Interfaces;

public interface IWhatFacultyAStudentAttendsRepository : IGenericMethods<WhatFacultyAStudentAttendsModel>
{
    Task<bool> RemoveByStudentIdAsync(int studentId);
}