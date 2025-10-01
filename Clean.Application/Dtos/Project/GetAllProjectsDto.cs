using Clean.Application.Dtos.Task;

namespace Clean.Application.Dtos.Project;

public class GetAllProjectsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<GetTaskDto> Tasks { get; init; } = new List<GetTaskDto>();

}