using Clean.Application.Abstractions;
using Clean.Application.Dtos.Task;
using Clean.Application.Dtos.TaskAssignment;
using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Data;

public class TaskAssignmentRepository : ITaskAssignmentRepository
{
    private readonly DataContext _context;

    public TaskAssignmentRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AssignTaskAsync(AssignTaskDto dto)
    {
     
        var taskToAssign = await _context.Tasks.FirstOrDefaultAsync(t =>
            t.Title.ToLower() == dto.TaskName.ToLower());
        if (taskToAssign is null)
        {
            throw new ArgumentException($"No task with the name {dto.TaskName} to assign");

        }
        var userToAssign = await _context.Users.FirstOrDefaultAsync(u => 
            u.Name.ToLower() == dto.UserName.ToLower());
        if (userToAssign is null)
        {
            throw new ArgumentException($"No user with name {dto.UserName} to assign");

        }
        
        var isPresentAlready = _context.TaskAssignments
            .Any(ta => ta.TaskId == taskToAssign.Id && ta.UserId == userToAssign.Id);

        if (isPresentAlready)
        {
            throw new ArgumentException(
                $"User with name {dto.UserName} already has task: {dto.TaskName}");
        }

        var taskAssignment = new TaskAssignment
        {
            TaskId = taskToAssign.Id,
            UserId = userToAssign.Id,
            AssignedDate = DateTime.UtcNow
        };
        await _context.TaskAssignments.AddAsync(taskAssignment);
        var isAdded = await _context.SaveChangesAsync();
        return isAdded > 0;
    }

    public async Task<bool> UpdateTaskAssignmentAsync(UpdateTaskAssignmentDto updateTask)
    {
        var toUpdate = await _context.TaskAssignments
            .FirstOrDefaultAsync(t => t.Id == updateTask.TaskAssignmentId);
        if (toUpdate is null)
        {
            throw new ArgumentException($"No task assignment with id {updateTask.TaskAssignmentId} to update");
        }

        var taskToUpdate = await _context.Tasks.FirstOrDefaultAsync(t =>
            t.Title.ToLower() ==  updateTask.TaskName.ToLower());
        if (taskToUpdate is null)
        {
            throw new ArgumentException($"No task with id {updateTask.TaskAssignmentId} to update");

        }
        
        
        var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => 
            u.Name.ToLower() == updateTask.UserName.ToLower());
        if (userToUpdate is null)
        {
            throw new ArgumentException($"No user with name {updateTask.UserName} to assign");

        }
        
        if (taskToUpdate is null)
        {
            throw new ArgumentException($"No task with id {updateTask.TaskAssignmentId} to assign");

        }

        var isPresentAlready = _context.TaskAssignments
            .Any(ta => ta.TaskId == taskToUpdate.Id && ta.UserId == userToUpdate.Id);

        if (isPresentAlready)
        {
            throw new ArgumentException(
                $"User with name {updateTask.UserName} already has task: {updateTask.TaskName}");
        }
        
        toUpdate.TaskId = taskToUpdate.Id;
        toUpdate.UserId = userToUpdate.Id;

        var isUpdated = await _context.SaveChangesAsync();

        return isUpdated > 0;
    }

    public async Task<TaskAssignment>? GetTaskAssignmentInfoAsync(int id)
    {
        var task = await _context.TaskAssignments
            .Include(t => t.Task)
            .ThenInclude(task1 => task1.CreatedUser)
            .Include(t => t.Task)
            .ThenInclude(y=> y.Project)
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (task is null)
        {
            throw new ArgumentException($"No task assignment with id: {id}");
        }

        return task;
    }

    public async Task<bool> DeleteTaskAssignmentAsync(int id)
    {
        var toDelete = await _context.TaskAssignments.FindAsync(id);
        if (toDelete is null)
        {
            throw new ArgumentException($"No task assignment with id {id} to delete");
        }

        _context.TaskAssignments.Remove(toDelete);
        var isDeleted = await _context.SaveChangesAsync();
        return isDeleted > 0;
    }

    public async Task<List<TaskAssignment>> GetTaskAssignmentByUserAsync(RequestTaskByUsernameDto dto)
    {
        var tasks = await _context.TaskAssignments
            .Include(ta => ta.User)
            .Include(ta=> ta.Task)
            .ThenInclude(t => t.Project)
            .Include(t=> t.Task)
            .ThenInclude(task => task.CreatedUser)
            .Where(ta => ta.User.Name.ToLower() == dto.UserName.ToLower())
            .ToListAsync();
        
        return tasks;
    }

    public async Task<List<TaskAssignment>> GetTaskAssignmentByTaskAsync(RequestAssignmentByTaskDto dto)
    {
        var list = await _context.TaskAssignments
            .Include(ta => ta.Task)
            .ThenInclude(t=> t.Project)
            .Include(assignment => assignment.Task)
            .ThenInclude(task => task.CreatedUser)
            .Include(ta=> ta.User)
            .Where(ta => ta.Task.Title.ToLower() == dto.TaskName.ToLower())
            .ToListAsync();

        return list;
    }
}