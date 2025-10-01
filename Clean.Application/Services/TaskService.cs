using Clean.Application.Abstractions;
using Clean.Application.Dtos.Project;
using Clean.Application.Dtos.Task;
using Clean.Application.Dtos.User;

namespace Clean.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskReadService _service;

    public TaskService(ITaskRepository taskRepository, ITaskReadService service)
    {
        _taskRepository = taskRepository;
        _service = service;
    }


    public async Task<Response<bool>> AddTaskAsync(AddTaskDto dto)
    {
        var task = new Domain.Entities.Task
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatedUserId = dto.CreatedUserId,
            DueDate = dto.DueDate.HasValue ? dto.DueDate : null,
            ProjectId = dto.ProjectId
        };
        var isAdded = await _taskRepository.AddTaskAsync(task);
        return new Response<bool>(isAdded);
    }

    public async Task<Response<List<GetTaskDto>>> GetTasksAsync()
    {
        var list = await _taskRepository.GetTasksAsync();
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
        return new Response<List<GetTaskDto>>(dtos);
    }

    public async Task<Response<GetTaskDto>>? GetTaskByIdAsync(int id)
    {
        var t = await _taskRepository.GetTaskByIdAsync(id)!;
        if (t is null)
        {
            throw new ArgumentException($"No task with id: {id}");
        }
        
        var dto =  new GetTaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            CreatedBy = t.CreatedUser.Name,
            CreatedDate = t.CreatedDate,
            DueDate = t.DueDate,
            InProject = t.Project.Name
        };

        return new Response<GetTaskDto>(dto);
    }

    public async Task<Response<bool>> UpdateTaskAsync(UpdateTaskDto dto)
    {
        var toUpdate = new Domain.Entities.Task
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate
        };

        var isUpdated = await _taskRepository.UpdateTaskAsync(toUpdate);
        return new Response<bool>(isUpdated);
    }

    public async Task<Response<bool>> DeleteTaskAsync(int id)
    {
        var isDeleted = await _taskRepository.DeleteTaskAsync(id);
        return new Response<bool>(isDeleted);
    }

    public async Task<Response<GetTaskByProject>> GetTaskByProjectAsync(RequestTasksByProjectDto dto)
    {
        var byName = new GetProjectByName { Name = dto.ProjectTitle };
        var byProject = await _service.GetTasksByProjectAsync(byName)!;
        if (byProject is null)
        {
            throw new ArgumentException($"No Project is named '{dto.ProjectTitle}'");
        }

        var result = new GetTaskByProject
        {
            ProjectName = byProject.Name,
            Tasks = byProject.Tasks.Select(t=> new GetTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedBy = t.CreatedUser.Name,
                CreatedDate = t.CreatedDate,
                InProject = t.Project.Name,
                DueDate = t.DueDate
            }).ToList()
        };

        return new Response<GetTaskByProject>(result);
    }

    public async Task<Response<GetTasksByUser>> GetTasksByUserAsync(RequestTaskByUsernameDto dto)
    {
        var response = await _service.GetTasksByUserAsync(dto.UserName)!;
        if (response is null)
        {
            throw new ArgumentException($"No user with name: {dto.UserName}");
        }

        return new Response<GetTasksByUser>(response);
    }

    public async Task<Response<List<GetTaskDto>>> GetTasksDueSoon(int days)
    {
        var response = await _service.GetTasksDueSoonAsync(days);
        return new Response<List<GetTaskDto>>(response);
    }

    public async Task<Response<List<GetTaskDto>>> GetOverdueTasksAsync()
    {
        var list = await _service.GetOverdueTasksAsync();
        return new Response<List<GetTaskDto>>(list);
    }

    public async Task<Response<List<GetTaskWithMonthDto>>> GetTasksByMonthsAsync()
    {
        var list = await _service.GetTasksByMonthsAsync();
        return new Response<List<GetTaskWithMonthDto>>(list);
    }

    public async Task<Response<List<GetMultiTasksDto>>> GetMultiAssignedTasksAsync()
    {
        var list = await _service.GetMultiAssignedTasksAsync();
        return new Response<List<GetMultiTasksDto>>(list);
    }

}