namespace Clean.Application.Dtos.Task;

public class GetTaskWithStatusDto : GetTaskDto
{
    public bool IsDone { get; set; }
}