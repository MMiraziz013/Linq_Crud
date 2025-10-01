namespace Clean.Application.Dtos.User;

public class GetUserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    // public ICollection<string> CreatedTasks { get; set; } = new List<string>();
    // public ICollection<string> Assignments { get; set; } = new List<string>();
}