namespace Clean.Domain.Entities;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }

    public bool IsDone { get; set; }
    
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    
    public int CreatedUserId { get; set; }
    public User CreatedUser { get; set; } = null!;
    public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();
}