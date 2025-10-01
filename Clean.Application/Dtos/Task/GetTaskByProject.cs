namespace Clean.Application.Dtos.Task;

public class GetTaskByProject
{
    public string ProjectName { get; set; }
    public IEnumerable<GetTaskDto> Tasks { get; set; } = new List<GetTaskDto>();
}