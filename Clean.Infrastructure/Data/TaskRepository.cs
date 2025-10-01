using Clean.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Data;

public class TaskRepository : ITaskRepository
{
    private readonly DataContext _context;

    public TaskRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<bool> AddTaskAsync(Domain.Entities.Task task)
    {
        await _context.Tasks.AddAsync(task);
        var isAdded = await _context.SaveChangesAsync();
        if (isAdded > 0)
        {
            return true;
        }

        throw new ArgumentException($"Error while adding the task");
    }

    public async Task<List<Domain.Entities.Task>> GetTasksAsync()
    {
        var list = await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.CreatedUser).ToListAsync();
        return list;
    }

    public async Task<Domain.Entities.Task>? GetTaskByIdAsync(int id)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.CreatedUser)
            .Where(t=> t.Id == id).ToListAsync();
        if (task is null)
        {
            throw new ArgumentException($"No task with id: {id}");
        }

        return task[0];
    }

    public async Task<bool> UpdateTaskAsync(Domain.Entities.Task dto)
    {
        var toUpdate = await _context.Tasks.FindAsync(dto.Id);
        if (toUpdate is null)
        {
            throw new ArgumentException($"No task with id: {dto.Id}");
        }

        toUpdate.Title = dto.Title;
        toUpdate.Description = dto.Description;
        toUpdate.DueDate = dto.DueDate;

        var isUpdated = await _context.SaveChangesAsync();
        if (isUpdated > 0)
        {
            return true;
        }
        
        throw new ArgumentException($"No task with id: {dto.Id}");
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var toDelete = await GetTaskByIdAsync(id)!;
        if (toDelete is null)
        {
            throw new ArgumentException($"No task with id: {id}");

        }

        _context.Remove(toDelete);
        var isDeleted = await _context.SaveChangesAsync();
        if (isDeleted > 0)
        {
            return true;
        }

        throw new ArgumentException("Error happened while deleting from database");
    }
}