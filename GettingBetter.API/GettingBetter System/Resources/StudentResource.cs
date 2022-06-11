using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Resources;

public class StudentResource
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } 
    public CoachResource Coach { get; set; }
}