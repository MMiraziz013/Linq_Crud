using Clean.Application.Dtos.Task;
using Clean.Application.Dtos.TaskAssignment;
using Clean.Application.Services;
using Clean.Domain.Entities;

namespace Clean.Application.Abstractions;

public interface ITaskAssignmentService
{
    public Task<Response<bool>> AssignTaskAsync(AssignTaskDto taskAssignment);
    public Task<Response<bool>> UpdateTaskAssignmentAsync(UpdateTaskAssignmentDto updateTask);
    public Task<Response<GetTaskAssignmentDto>>? GetTaskAssignmentInfoAsync(int id);
    public Task<Response<bool>> DeleteTaskAssignmentAsync(int id);
    
    public Task<Response<List<GetTaskAssignmentDto>>> GetTaskAssignmentByUserAsync(RequestTaskByUsernameDto dto);
    public Task<Response<List<GetTaskAssignmentDto>>> GetTaskAssignmentByTaskAsync(RequestAssignmentByTaskDto dto);
}