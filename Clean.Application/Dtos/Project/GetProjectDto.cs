namespace Clean.Application.Dtos.Project;

public class GetProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<Domain.Entities.Task> Tasks { get; init; } = new List<Domain.Entities.Task>();
}