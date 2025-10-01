using Clean.Application.Dtos.Project;
using Clean.Application.Dtos.Task;

namespace Clean.Application.Abstractions;

public interface ITaskReadService
{
    public Task<GetProjectDto>? GetTasksByProjectAsync(GetProjectByName title);
    public Task<GetTasksByUser>? GetTasksByUserAsync(string name);
    public Task<List<GetTaskDto>> GetTasksDueSoonAsync(int days);

    public Task<List<GetTaskDto>> GetOverdueTasksAsync();

    public Task<List<GetTaskWithMonthDto>> GetTasksByMonthsAsync();

    public Task<List<GetMultiTasksDto>> GetMultiAssignedTasksAsync();
}