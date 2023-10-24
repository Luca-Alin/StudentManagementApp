using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Data;
using StudentManagementApp.Identity;
using StudentManagementApp.Interfaces;
using StudentManagementApp.Models;
using StudentManagementApp.Models.Student;

namespace StudentManagementApp.Controllers;

[Route("api/student/")]
[ApiController]
public class StudentController : Controller
{
    private readonly IStudentRepository _studentRepository;
    private readonly IWhatFacultyAStudentAttendsRepository _whatFacultyAStudentAttendsRepository;
    private readonly IFacultyRepository _facultyRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IGradeRepository _gradeRepository;

    public StudentController(IStudentRepository studentRepository,
        IWhatFacultyAStudentAttendsRepository whatFacultyAStudentAttendsRepository,
        IFacultyRepository facultyRepository,
        IAddressRepository addressRepository,
        IGradeRepository gradeRepository)
    {
        _studentRepository = studentRepository;
        _whatFacultyAStudentAttendsRepository = whatFacultyAStudentAttendsRepository;
        _facultyRepository = facultyRepository;
        _addressRepository = addressRepository;
        _gradeRepository = gradeRepository;
    }

    [HttpGet("studentById")]
    [Authorize]
    public async Task<StudentModel?> Student()
    {
        string? authorizationHeader = Request.Headers["Authorization"];
        var id = JwtParser.ParseJwt(authorizationHeader);
        if (id == -1)
            return null;

        return await _studentRepository.GetByIdAsync(id);
    }

    [HttpGet("byFaculty/{facultyId}")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IEnumerable<StudentDto>> GetStudentsByFaculty(int facultyId)
    {
        return await _studentRepository.GetStudentsByFaculty(facultyId);
    }

    [HttpPost("add")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<ActionResult> AddStudent(StudentDto dto)
    {
        if (dto.Email == null) return BadRequest("Field email is required");
        if (dto.FirstName == null) return BadRequest("Field first name is required");
        if (dto.LastName == null) return BadRequest("Field last name is required");
        if (dto.PhoneNumber == null) return BadRequest("Field phone number is required");
        if (dto.DateOfBirth == null) return BadRequest("Field date of birth is required");
        if (dto.Password == null) return BadRequest("Field password is required");
        if (dto.Address == null) return BadRequest("Field address is required");
        if (dto.Faculties == null) return BadRequest("Field faculties is required");

        var student = new StudentModel
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth.Value,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        var ok = _studentRepository.Add(student);

        foreach (var faculty in dto.Faculties)
        {
            var whatFaculty = new WhatFacultyAStudentAttendsModel
            {
                Student = (await _studentRepository.GetByIdAsync(
                    await _studentRepository.GetIdByEmailAsync(student.Email)))!,
                Faculty = await _facultyRepository.GetByIdAsync(faculty.Id)
            };
            _whatFacultyAStudentAttendsRepository.Add(whatFaculty);
        }

        return ok ? Ok($"Student {student.FirstName} {student.LastName} added") : BadRequest("Student not added");
    }

    [HttpGet("all")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IEnumerable<StudentDto>> GetAllStudents()
    {
        var students = await _studentRepository.GetAll();
        var studentDtos = new List<StudentDto>();
        foreach (var s in students)
        {
            var faculties = _facultyRepository.GetWhatFacultiesAStudentAttends(s.Id);
            studentDtos.Add(new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                Faculties = faculties.Result.ToList()
            });
        }

        return studentDtos;
    }

    [HttpDelete("delete/{id}")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<ActionResult> DeleteStudent(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        await _whatFacultyAStudentAttendsRepository.RemoveByStudentIdAsync(id);
        await _gradeRepository.DeleteByStudentId(id);

        if (student == null)
            return BadRequest("Student not found");

        var address = student.Address;
        var ok = _studentRepository.Delete(student) && _addressRepository.Delete(address);

        return ok ? Ok($"Student {student.FirstName} {student.LastName} deleted") : BadRequest("Student not deleted");
    }

    [HttpPut("update")]
    [Authorize("admin", Policy = IdentityData.AdminUserPolicyName)]
    public async Task<ActionResult> UpdateStudent(StudentDto dto)
    {
        var student = await _studentRepository.GetByIdAsync(dto.Id);
        if (student == null)
            return BadRequest("Student not found");
        
        if (dto.Email != null) student.Email = dto.Email;
        if (dto.FirstName != null) student.FirstName = dto.FirstName;
        if (dto.LastName != null) student.LastName = dto.LastName;
        if (dto.PhoneNumber != null) student.PhoneNumber = dto.PhoneNumber;
        if (dto.DateOfBirth != null)
            student.DateOfBirth = dto.DateOfBirth!.Value;
        if (dto.Password != null) student.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        if (dto.Address != null) student.Address = dto.Address;
        
        var ok = _studentRepository.Update(student);

        var w = await _facultyRepository.GetWhatFacultiesAStudentAttends(student.Id);
        List<FacultyModel>
            whatFacultiesAStudentAttends = new List<FacultyModel>();
        foreach (var f in w)
        {
            whatFacultiesAStudentAttends.Add(f);
        }
        
        if (dto.Faculties != null)
        {    foreach (var faculty in dto.Faculties)
            {
                if (!whatFacultiesAStudentAttends.Contains(faculty))
                    _whatFacultyAStudentAttendsRepository.Add(new()
                    {
                        Student = student,
                        Faculty = (await _facultyRepository.GetByIdAsync(faculty.Id))!
                    });
            }

            foreach (var faculty in whatFacultiesAStudentAttends)
            {
                if (!dto.Faculties.Contains(faculty))
                    await _whatFacultyAStudentAttendsRepository.RemoveByStudentIdAsync(student.Id);
            }
        }
        
        return ok ? Ok($"Student {student.FirstName} {student.LastName} updated") : BadRequest("Student not updated");
    }
}