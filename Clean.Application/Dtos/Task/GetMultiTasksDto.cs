using Clean.Application.Dtos.User;

namespace Clean.Application.Dtos.Task;

public class GetMultiTasksDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
    
    public string InProject { get; set; }
    
    public string CreatedBy { get; set; }

    public List<GetUserDto> AssignedUsers { get; set; }
}