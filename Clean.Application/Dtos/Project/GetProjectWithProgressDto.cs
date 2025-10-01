using Clean.Application.Dtos.Task;

namespace Clean.Application.Dtos.Project;

public class GetProjectWithProgressDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Progress { get; set; }
    public ICollection<GetTaskWithStatusDto> Tasks { get; init; } = new List<GetTaskWithStatusDto>();
}