namespace Clean.Application.Dtos.TaskAssignment;

public class UpdateTaskAssignmentDto
{
    public int TaskAssignmentId { get; set; }
    public string UserName { get; set; }
    public string TaskName { get; set; }
}