using Clean.Application.Abstractions;
using Clean.Application.Dtos.Task;
using Clean.Application.Dtos.User;
using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<bool> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        var isAdded = await _context.SaveChangesAsync();
        if (isAdded > 0) return true;

        throw new ArgumentException("User was not added");
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var list = await _context.Users.ToListAsync();
        return list;
    }

    public async Task<User>? GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is not null)
        {
            return user;
        }

        throw new ArgumentException($"No user with id: {id}");
    }

    public async Task<User>? GetUserByNameAsync(string name)
    {
        var user = await _context.Users
            .Where(u => u.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();

        if (user is null)
        {
            throw new ArgumentException("No user with such credentials");
        }

        return user;
    }

    public async Task<bool> UpdateUserAsync(User dto)
    {
        var toUpdate = await _context.Users.FindAsync(dto.Id);
        if (toUpdate is not null)
        {
            toUpdate.Email = dto.Email;
            toUpdate.Name = dto.Name;
            var isUpdated = await _context.SaveChangesAsync();
            if (isUpdated > 0)
            {
                return true;
            }
        }

        throw new ArgumentException($"No User with id: {dto.Id}");
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var toDelete = await _context.Users.FindAsync(id);
        if (toDelete is not null)
        {
            _context.Users.Remove(toDelete);
            var isDeleted = await _context.SaveChangesAsync();
            if (isDeleted > 0)
            {
                return true;
            }

        }
        
        throw new ArgumentException($"No user to delete with id: {id}");
    }

    public async Task<User>? GetUserWithMostTasksAsync()
    {
        var list = await _context.Users
            .Include(u=> u.Assignments)
            .Where(u => u.Assignments.Count > 0)
            .OrderByDescending(u => u.Assignments.Count)
            .Take(1).ToListAsync();
        if (list.Count > 0)
        {
            return list[0];
        }

        throw new Exception("No users with tasks!");
    }

    public async Task<List<UserTaskCountDto>> GetUsersWithoutTasksAsync()
    {
        var list = await _context.Users
            .Include(u => u.Assignments)
            .Where(u => u.Assignments.Count == 0).ToListAsync();

        var dtos = list.Select(u => new UserTaskCountDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            TaskCount = u.Assignments.Count,
            RegistrationDate = u.RegistrationDate
        }).ToList();

        return dtos;
    }

    public async Task<List<UserTaskCountDto>> GetTopFiveActiveUsersAsync()
    {
        var list = await _context.Users
            .Include(u => u.Assignments)
            .OrderByDescending(u => u.Assignments.Count).Take(5).ToListAsync();

        var dtos = list.Select(u => new UserTaskCountDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            TaskCount = u.Assignments.Count,
            RegistrationDate = u.RegistrationDate
        }).ToList();

        return dtos;
    }

    public async Task<List<GetUserWithTasksDto>> GetNewUsersWithTasks()
    {
        var list = await _context.TaskAssignments
            .Include(ta => ta.User)
            .ThenInclude(u=> u.Assignments)
            .Include(ta=> ta.Task)
            .ThenInclude(t=> t.Project)
            .Where(ta => ta.User.RegistrationDate >= DateTime.UtcNow.AddDays(-30))
            .ToListAsync();

        var dtos = list.Select(ta => new GetUserWithTasksDto
        {
            Id = ta.UserId,
            Name = ta.User.Name,
            Email = ta.User.Email,
            RegistrationDate = ta.User.RegistrationDate,
            AssignedTasks = ta.User.Assignments.Select(t => new GetTaskDto
            {
                Id = t.Id,
                Title = t.Task.Title,
                Description = t.Task.Description,
                CreatedBy = $"Created by user with id: {t.Task.CreatedUserId}",
                CreatedDate = t.Task.CreatedDate,
                DueDate = t.Task.DueDate,
                InProject = t.Task.Project.Name
            }).ToList(),
        }).ToList();

        return dtos;
    }

    public async Task<List<GetUserWithTasksDto>> GetUserWithOverdueTasks()
    {
        var users = await _context.Users
            .Where(u => u.Assignments.Any(a => a.Task.DueDate < DateTime.UtcNow && !a.Task.IsDone))
            .Select(u => new GetUserWithTasksDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                RegistrationDate = u.RegistrationDate,
                AssignedTasks = u.Assignments
                    .Where(a => a.Task.DueDate < DateTime.UtcNow && !a.Task.IsDone)
                    .Select(a => new GetTaskDto
                    {
                        Id = a.Task.Id,
                        Title = a.Task.Title,
                        Description = a.Task.Description,
                        CreatedBy = a.Task.CreatedUser.Name,
                        CreatedDate = a.Task.CreatedDate,
                        DueDate = a.Task.DueDate,
                        InProject = a.Task.Project.Name
                    }).ToList()
            })
            .ToListAsync();

        return users;
    }

}