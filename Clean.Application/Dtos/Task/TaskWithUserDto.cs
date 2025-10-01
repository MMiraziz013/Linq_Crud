namespace Clean.Application.Dtos.Task;

public class TaskWithUserDto
{
    public Domain.Entities.User User { get; set; }
    public ICollection<GetTaskDto> Tasks { get; set; } = new List<GetTaskDto>();
}