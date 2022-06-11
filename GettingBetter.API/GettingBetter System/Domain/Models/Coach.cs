namespace LearningCenter.API.Learning.Domain.Models;

public class Coach
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SelectedGame { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    // Relationships

    public IList<Student> Students { get; set; } = new List<Student>();
}




