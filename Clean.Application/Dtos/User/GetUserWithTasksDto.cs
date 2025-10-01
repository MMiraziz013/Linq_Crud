using Clean.Application.Dtos.Task;

namespace Clean.Application.Dtos.User;

public class GetUserWithTasksDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public List<GetTaskDto> AssignedTasks { get; set; }
}