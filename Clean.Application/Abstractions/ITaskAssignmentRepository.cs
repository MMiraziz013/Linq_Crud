using Clean.Application.Dtos.Task;
using Clean.Application.Dtos.TaskAssignment;
using Clean.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Clean.Application.Abstractions;

public interface ITaskAssignmentRepository
{
    public Task<bool> AssignTaskAsync(AssignTaskDto taskAssignment);
    public Task<bool> UpdateTaskAssignmentAsync(UpdateTaskAssignmentDto updateTask);
    public Task<TaskAssignment>? GetTaskAssignmentInfoAsync(int id);
    public Task<bool> DeleteTaskAssignmentAsync(int id);
    public Task<List<TaskAssignment>> GetTaskAssignmentByUserAsync(RequestTaskByUsernameDto dto);
    public Task<List<TaskAssignment>> GetTaskAssignmentByTaskAsync(RequestAssignmentByTaskDto dto);
}