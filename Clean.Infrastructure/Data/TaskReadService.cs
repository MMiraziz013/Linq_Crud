using Clean.Application.Abstractions;
using Clean.Application.Dtos.Project;
using Clean.Application.Dtos.Task;
using Clean.Application.Dtos.User;
using Clean.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Data;

public class TaskReadService : ITaskReadService
{
    private readonly DataContext _context;
    private readonly IProjectRepository _project;

    public TaskReadService(DataContext context, IProjectRepository project)
    {
        _context = context;
        _project = project;
    }
    public async Task<GetProjectDto> GetTasksByProjectAsync(GetProjectByName title)
    {
        var project = await _project.GetProjectByNameAsync(title)!;
        if (project is null)
        {
            throw new ArgumentNullException($"No project with the name: {title}");
        }

        return project;
    }

    public async Task<GetTasksByUser> GetTasksByUserAsync(string name)
    {
        var tasks = await _context.TaskAssignments
            .Include(t => t.User)
            .Include(t => t.Task)
            .ThenInclude(ta=> ta.Project)
            .Where(t=> t.User.Name == name)
            .ToListAsync();

        if (tasks.Count == 0)
        {
            throw new ArgumentException($"No user with name {name}");
        }

        var user = tasks[0].User;

        var taskDtos = tasks.Select(t => new GetTaskDto
        {
            Id = t.Id,
            Title = t.Task.Title,
            Description = t.Task.Description,
            CreatedBy = user.Name,
            CreatedDate = t.Task.CreatedDate,
            InProject = t.Task.Project.Name,
            DueDate = t.Task.DueDate
        }).ToList();

        var dto = new GetTasksByUser
        {
            Id = user.Id,
            UserName = user.Name,
            Email = user.Email,
            UserRegistrationDate = user.RegistrationDate,
            Tasks = taskDtos
        };

        return dto;
    }


    public async Task<List<GetTaskDto>> GetTasksDueSoonAsync(int days)
    {
        var tasks = await  _context.Tasks
            .Include(t => t.CreatedUser)
            .Include(t => t.Project)
            .Where(t => t.DueDate <= DateTime.UtcNow.AddDays(days)).ToListAsync();

        var dtos = tasks.Select(t => new GetTaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            CreatedBy = t.CreatedUser.Name,
            CreatedDate = t.CreatedDate,
            DueDate = t.DueDate,
            InProject = t.Project.Name
        }).ToList();

        return dtos;
    }

    public async Task<List<GetTaskDto>> GetOverdueTasksAsync()
    {
        var list = await _context.Tasks
            .Include(t => t.CreatedUser)
            .Include(t => t.Project)
            .Where(t => t.DueDate < DateTime.UtcNow && t.IsDone == false)
            .ToListAsync();
        
        var dtos = list.Select(t => new GetTaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            CreatedBy = t.CreatedUser.Name,
            CreatedDate = t.CreatedDate,
            DueDate = t.DueDate,
            InProject = t.Project.Name
        }).ToList();

        return dtos;
    }

    public async Task<List<GetTaskWithMonthDto>> GetTasksByMonthsAsync()
    {
        var rawData = await _context.Tasks
            .Include(t => t.CreatedUser)
            .Include(t => t.Project)
            .Where(t => t.DueDate.HasValue)
            .GroupBy(t => new { t.DueDate!.Value.Year, t.DueDate!.Value.Month })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Tasks = g.Select(t => new GetTaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedDate = t.CreatedDate,
                    DueDate = t.DueDate,
                    InProject = t.Project.Name,
                    CreatedBy = t.CreatedUser.Name
                }).ToList()
            })
            .OrderBy(g => g.Year)
            .ThenBy(g => g.Month)
            .ToListAsync();

        var dtos = rawData.Select(x => new GetTaskWithMonthDto
        {
            Year = x.Year,
            Month = new DateTime(x.Year, x.Month, 1).ToString("MMMM"),
            Tasks = x.Tasks
        }).ToList();

        return dtos;
    }



    public async Task<List<GetMultiTasksDto>> GetMultiAssignedTasksAsync()
    {
        var list = await _context.TaskAssignments
            .Include(ta => ta.Task)
            .ThenInclude(t => t.Project)
            .Include(ta => ta.Task)
            .ThenInclude(t => t.CreatedUser)
            .Include(ta => ta.User)
            .GroupBy(ta => ta.TaskId)
            .Where(g => g.Count() > 1)
            .Select(g => new GetMultiTasksDto
            {
                Id = g.First().Task.Id,
                Title = g.First().Task.Title,
                Description = g.First().Task.Description,
                CreatedDate = g.First().Task.CreatedDate,
                DueDate = g.First().Task.DueDate,
                InProject = g.First().Task.Project.Name,
                CreatedBy = g.First().Task.CreatedUser.Name,
                AssignedUsers = g.Select(x => new GetUserDto
                {
                    Id = x.User.Id,
                    Name = x.User.Name,
                    Email = x.User.Email,
                    RegistrationDate = x.User.RegistrationDate
                }).ToList()
            })
            .ToListAsync();

        return list;
    }
}