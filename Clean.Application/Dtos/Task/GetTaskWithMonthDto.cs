namespace Clean.Application.Dtos.Task;

public class GetTaskWithMonthDto
{
    public int Year { get; set; }
    public string Month { get; set; }
    public List<GetTaskDto> Tasks { get; set; }
}