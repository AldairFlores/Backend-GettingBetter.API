using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Shared.Domain.Repositories;

namespace LearningCenter.API.Learning.Services;

public class CoachService : ICoachService
{
    private readonly ICoachRepository _coachRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CoachService(ICoachRepository coachRepository, IUnitOfWork unitOfWork)
    {
        _coachRepository = coachRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Coach>> ListAsync()
    {
        return await _coachRepository.ListAsync();
    }

    public async Task<CoachResponse> SaveAsync(Coach coach)
    {
        try
        {
            await _coachRepository.AddAsync(coach);
            await _unitOfWork.CompleteAsync();
            return new CoachResponse(coach);
        }
        catch (Exception e)
        {
            return new CoachResponse($"An error occurred while saving the coach: {e.Message}");
        }
    }

    public async Task<CoachResponse> UpdateAsync(int id, Coach coach)
    {
        var existingCoach = await _coachRepository.FindByIdAsync(id);

        if (existingCoach == null)
            return new CoachResponse("Coach not found.");

        existingCoach.FirstName = coach.FirstName;

        try
        {
            _coachRepository.Update(existingCoach);
            await _unitOfWork.CompleteAsync();

            return new CoachResponse(existingCoach);
        }
        catch (Exception e)
        {
            return new CoachResponse($"An error occurred while updating the coach: {e.Message}");
        }
    }

    public async Task<CoachResponse> DeleteAsync(int id)
    {
        var existingCoach = await _coachRepository.FindByIdAsync(id);

        if (existingCoach == null)
            return new CoachResponse("Coach not found.");

        try
        {
            _coachRepository.Remove(existingCoach);
            await _unitOfWork.CompleteAsync();

            return new CoachResponse(existingCoach);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new CoachResponse($"An error occurred while deleting the coach: {e.Message}");
        }
    }
}