using Clean.Application.Abstractions;
using Clean.Application.Dtos.Project;
using Clean.Application.Dtos.Task;
using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Clean.Infrastructure.Data;

public class ProjectRepository : IProjectRepository
{
    private readonly DataContext _context;

    public ProjectRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> AddProjectAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        var isAdded = await _context.SaveChangesAsync();
        return isAdded > 0;
    }

    public async Task<List<Project>> GetAllProjectsAsync()
    {
        var list = await _context.Projects
            .Include(p => p.Tasks)
            .ThenInclude(t=> t.CreatedUser).ToListAsync();
        return list;
    }

    public async Task<Project> GetProjectByIdAsync(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Tasks)
            .ThenInclude(t=> t.CreatedUser)
            .Where(p => p.Id == id).ToListAsync();
        return project[0];
    }

    public async Task<bool> UpdateProjectAsync(Project project)
    {
        var toUpdate = await _context.Projects.FindAsync(project.Id);
        if (toUpdate is null)
        {
            throw new ArgumentException($"No project with id: {project.Id} to Update");
        }

        toUpdate.Name = project.Name;
        toUpdate.Description = project.Description;
        toUpdate.EndDate = project.EndDate;
        toUpdate.StartDate = project.StartDate;

        var isUpdated = await _context.SaveChangesAsync();
        return isUpdated > 0;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var toDelete = await _context.Projects.FindAsync(id);
        if (toDelete is null)
        {
            throw new ArgumentException($"No project with id: {id} to Delete");
        }

        _context.Projects.Remove(toDelete);
        var isDeleted = await _context.SaveChangesAsync();
        return isDeleted > 0;
    }

    public async Task<Project> GetProjectWithMostTasksAsync()
    {
        var list = await _context.Projects
            .Include(p => p.Tasks)
            .ThenInclude(t=> t.CreatedUser)
            .OrderByDescending(p => p.Tasks.Count)
            .ToListAsync();
        return list[0];
    }

    public async Task<GetProjectDto> GetProjectByNameAsync(GetProjectByName dto)
    {
        var project = await _context.Projects
            .Include(p => p.Tasks)
            .ThenInclude(t => t.CreatedUser)
            .Include(p => p.Tasks)
            .ThenInclude(t => t.Project) 
            .Where(p => p.Name.ToLower() == dto.Name.ToLower())
            .Select(p => new GetProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Tasks = p.Tasks
            }).Take(1).ToListAsync();

        if (project.Count == 0)
        {
            throw new ArgumentException($"No project with title: {dto.Name}");
        }

        return project[0];
    }
    
    public async Task<List<GetProjectWithAvDurationDto>> GetProjectsWithAverageDurationAsync()
    {
        var list = await _context.Projects
            .Select(p => new GetProjectWithAvDurationDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                
                AverageDurationInDays = p.Tasks
                    .Where(t => t.DueDate != null)
                    .Select(t => (double?)(t.DueDate!.Value - t.CreatedDate).TotalDays)
                    .Average() ?? 0
            })
            .ToListAsync();
        
        return list;
    }

    public async Task<List<GetProjectWithProgressDto>> GetProjectWithProgressAsync()
    {
        var list = await _context.Projects
            .Include(p => p.Tasks)
            .ToListAsync();

        var dtos = list.Select(p => new GetProjectWithProgressDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            CreatedDate = p.CreatedDate,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            Progress = p.Tasks.Count == 0 ? "0%" : ((double)p.Tasks.Count(t => t.IsDone) / p.Tasks.Count * 100).ToString("F2") + "%",
            Tasks = p.Tasks.Select(t => new GetTaskWithStatusDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedDate = t.CreatedDate,
                DueDate = t.DueDate,
                IsDone = t.IsDone
            }).ToList()
        }).ToList();

        return dtos;
    }
}