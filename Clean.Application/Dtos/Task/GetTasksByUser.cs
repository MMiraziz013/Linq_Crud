using Clean.Domain.Entities;

namespace Clean.Application.Dtos.Task;

public class GetTasksByUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime UserRegistrationDate { get; set; }
    public ICollection<GetTaskDto> Tasks { get; set; } = new List<GetTaskDto>();
}