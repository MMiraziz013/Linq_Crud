namespace Clean.Application.Abstractions;

public interface ITaskRepository
{
    public Task<bool> AddTaskAsync(Domain.Entities.Task user);
    public Task<List<Domain.Entities.Task>> GetTasksAsync();
    public Task<Domain.Entities.Task>? GetTaskByIdAsync(int id);
    public Task<bool> UpdateTaskAsync(Domain.Entities.Task dto);
    public Task<bool> DeleteTaskAsync(int id);
}