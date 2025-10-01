namespace Clean.Application.Dtos.Task;

public class AddTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
    
    public int ProjectId { get; set; }
    
    public int CreatedUserId { get; set; }
}