using LearningCenter.API.GettingBetter_System.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Shared.Domain.Repositories;

namespace LearningCenter.API.GettingBetter_System.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICoachRepository _coachRepository;

    public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork, ICoachRepository coachRepository)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
        _coachRepository = coachRepository;
    }

    public async Task<IEnumerable<Student>> ListAsync()
    {
        return await _studentRepository.ListAsync();
    }

    public async Task<IEnumerable<Student>> ListByCoachIdAsync(int coachId)
    {
        return await _studentRepository.FindByCoachIdAsync(coachId);
    }

    public async Task<StudentResponse> SaveAsync(Student student)
    {
        // Validate CoachId

        var existingCoach = await _coachRepository.FindByIdAsync(student.CoachId);

        if (existingCoach == null)
            return new StudentResponse("Invalid Coach");
        
        // Validate 

       

        try
        {
            // Add Student
            await _studentRepository.AddAsync(student);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new StudentResponse(student);

        }
        catch (Exception e)
        {
            // Error Handling
            return new StudentResponse($"An error occurred while saving the student: {e.Message}");
        }

        
    }

    public async Task<StudentResponse> UpdateAsync(int studentId, Student student)
    {
        var existingStudent = await _studentRepository.FindByIdAsync(studentId);
        
        // Validate Student

        if (existingStudent == null)
            return new StudentResponse("Student not found.");

        // Validate CoachId

        var existingCoach = await _coachRepository.FindByIdAsync(student.CoachId);

        if (existingCoach == null)
            return new StudentResponse("Invalid Coach");

        try
        {
            _studentRepository.Update(existingStudent);
            await _unitOfWork.CompleteAsync();

            return new StudentResponse(existingStudent);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new StudentResponse($"An error occurred while updating the student: {e.Message}");
        }

    }

    public async Task<StudentResponse> DeleteAsync(int studentId)
    {
        var existingStudent = await _studentRepository.FindByIdAsync(studentId);
        
        // Validate Student

        if (existingStudent == null)
            return new StudentResponse("Student not found.");
        
        try
        {
            _studentRepository.Remove(existingStudent);
            await _unitOfWork.CompleteAsync();

            return new StudentResponse(existingStudent);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new StudentResponse($"An error occurred while deleting the student: {e.Message}");
        }

    }
}