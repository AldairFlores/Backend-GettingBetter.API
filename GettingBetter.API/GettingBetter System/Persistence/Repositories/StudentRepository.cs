using LearningCenter.API.GettingBetter_System.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.GettingBetter_System.Persistence.Repositories;

public class StudentRepository : BaseRepository, IStudentRepository
{
    public StudentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Student>> ListAsync()
    {
        return await _context.Students
            .Include(p => p.Coach)
            .ToListAsync();
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public async Task<Student> FindByIdAsync(int studentId)
    {
        return await _context.Students
            .Include(p => p.Coach)
            .FirstOrDefaultAsync(p => p.Id == studentId);
        
    }

  

    public async Task<IEnumerable<Student>> FindByCoachIdAsync(int coachId)
    {
        return await _context.Students
            .Where(p => p.CoachId == coachId)
            .Include(p => p.Coach)
            .ToListAsync();
    }

    public void Update(Student student)
    {
        _context.Students.Update(student);
    }

    public void Remove(Student student)
    {
        _context.Students.Remove(student);
    }
}