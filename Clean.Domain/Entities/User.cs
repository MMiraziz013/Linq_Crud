namespace Clean.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    
    public string HashedPassword { get; set; }
    public ICollection<Task> CreatedTasks { get; set; } = new List<Task>();
    public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();
}