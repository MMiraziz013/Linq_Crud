using Clean.Application.Abstractions;
using Clean.Application.Dtos.Project;
using Clean.Application.Dtos.Task;
using Clean.Domain.Entities;

namespace Clean.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;

    public ProjectService(IProjectRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response<bool>> AddProjectAsync(AddProjectDto project)
    {
        var toAdd = new Project
        {
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
        };

        var isAdded = await _repository.AddProjectAsync(toAdd);
        return new Response<bool>(isAdded);
    }

    public async Task<Response<List<GetAllProjectsDto>>> GetAllProjectsAsync()
    {
        var list = await _repository.GetAllProjectsAsync();
        var dtos = list.Select(p => new GetAllProjectsDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            CreatedDate = p.CreatedDate,
            Tasks = p.Tasks.Select(t=> new GetTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedDate = t.CreatedDate,
                DueDate = t.DueDate,
                InProject = p.Name,
                CreatedBy = t.CreatedUser.Name
            }).ToList()
        }).ToList();

        return new Response<List<GetAllProjectsDto>>(dtos);
    }

    public async Task<Response<GetAllProjectsDto>> GetProjectByIdAsync(int id)
    {
        var p = await _repository.GetProjectByIdAsync(id);
        var dto = new GetAllProjectsDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            CreatedDate = p.CreatedDate,
            Tasks = p.Tasks.Select(t=> new GetTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedDate = t.CreatedDate,
                DueDate = t.DueDate,
                InProject = p.Name,
                CreatedBy = t.CreatedUser.Name
            }).ToList()
        };

        return new Response<GetAllProjectsDto>(dto);
    }

    public async Task<Response<bool>> UpdateProjectAsync(UpdateProjectDto project)
    {
        var toUpdate = new Project
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate
            
        };

        var isUpdated = await _repository.UpdateProjectAsync(toUpdate);
        return new Response<bool>(isUpdated);
    }

    public async Task<Response<bool>> DeleteProjectAsync(int id)
    {
        var isDeleted = await _repository.DeleteProjectAsync(id);
        return new Response<bool>(isDeleted);
    }

    public async Task<Response<GetAllProjectsDto>> GetProjectWithMostTaskAsync()
    {
        var p = await _repository.GetProjectWithMostTasksAsync();
        var dto = new GetAllProjectsDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            Tasks = p.Tasks.Select(t=> new GetTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedDate = t.CreatedDate,
                DueDate = t.DueDate,
                InProject = p.Name,
                CreatedBy = t.CreatedUser.Name
            }).ToList()
        };

        return new Response<GetAllProjectsDto>(dto);
    }

    public async Task<Response<GetAllProjectsDto>>? GetProjectByNameAsync(GetProjectByName dto)
    {
        var p = await _repository.GetProjectByNameAsync(dto)!;
        var project = new GetAllProjectsDto()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            Tasks = p.Tasks.Select(t=> new GetTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedDate = t.CreatedDate,
                DueDate = t.DueDate,
                InProject = p.Name,
                CreatedBy = t.CreatedUser.Name
            }).ToList()
        };

        return new Response<GetAllProjectsDto>(project);
    }

    public async Task<Response<List<GetProjectWithAvDurationDto>>> GetProjectsWithAverageDurationAsync()
    {
        var list = await _repository.GetProjectsWithAverageDurationAsync();
        return new Response<List<GetProjectWithAvDurationDto>>(list);
    }

    public async Task<Response<List<GetProjectWithProgressDto>>> GetProjectWithProgressAsync()
    {
        var list = await _repository.GetProjectWithProgressAsync();
        return new Response<List<GetProjectWithProgressDto>>(list);
    }
}