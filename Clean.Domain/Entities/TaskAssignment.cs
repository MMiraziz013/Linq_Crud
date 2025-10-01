namespace Clean.Domain.Entities;

public class TaskAssignment
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public DateTime AssignedDate { get; set; }

    public Task Task { get; set; } = null!;
    public User User { get; set; } = null!;
}