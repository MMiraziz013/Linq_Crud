using Clean.Application.Dtos.Project;
using Clean.Application.Dtos.Task;
using Clean.Application.Services;

namespace Clean.Application.Abstractions;

public interface ITaskService
{
    public Task<Response<bool>> AddTaskAsync(AddTaskDto user);
    public Task<Response<List<GetTaskDto>>> GetTasksAsync();
    public Task<Response<GetTaskDto>>? GetTaskByIdAsync(int id);
    public Task<Response<bool>> UpdateTaskAsync(UpdateTaskDto dto);
    public Task<Response<bool>> DeleteTaskAsync(int id);

    public Task<Response<GetTaskByProject>> GetTaskByProjectAsync(RequestTasksByProjectDto dto);
    public Task<Response<GetTasksByUser>> GetTasksByUserAsync(RequestTaskByUsernameDto dto);
    public Task<Response<List<GetTaskDto>>> GetTasksDueSoon(int days);
    
    public Task<Response<List<GetTaskDto>>> GetOverdueTasksAsync();

    public Task<Response<List<GetTaskWithMonthDto>>> GetTasksByMonthsAsync();

    public Task<Response<List<GetMultiTasksDto>>> GetMultiAssignedTasksAsync();
}