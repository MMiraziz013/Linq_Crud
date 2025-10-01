namespace Clean.Application.Dtos.User;

public class UserTaskCountDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int TaskCount { get; set; }
    public DateTime RegistrationDate { get; set; }
}