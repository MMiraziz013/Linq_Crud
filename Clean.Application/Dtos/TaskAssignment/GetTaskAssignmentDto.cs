using Clean.Application.Dtos.Task;
using Clean.Application.Dtos.User;

namespace Clean.Application.Dtos.TaskAssignment;

public class GetTaskAssignmentDto
{
    public int TaskAssignmentId { get; set; }
    public GetUserDto User { get; set; }
    public GetTaskDto Task { get; set; }
    public DateTime AssignedDate { get; set; }
}