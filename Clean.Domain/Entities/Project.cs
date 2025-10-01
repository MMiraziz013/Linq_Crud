namespace Clean.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<Domain.Entities.Task> Tasks { get; set; } = new List<Task>();
}