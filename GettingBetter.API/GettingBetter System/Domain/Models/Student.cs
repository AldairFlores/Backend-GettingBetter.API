namespace LearningCenter.API.Learning.Domain.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public int CoachId { get; set; }
    public Coach Coach { get; set; }
}