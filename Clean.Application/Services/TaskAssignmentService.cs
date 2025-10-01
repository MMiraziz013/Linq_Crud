using Clean.Application.Abstractions;
using Clean.Application.Dtos.Task;
using Clean.Application.Dtos.TaskAssignment;
using Clean.Application.Dtos.User;
using Clean.Domain.Entities;

namespace Clean.Application.Services;

public class TaskAssignmentService : ITaskAssignmentService
{
    private readonly ITaskAssignmentRepository _repository;

    public TaskAssignmentService(ITaskAssignmentRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response<bool>> AssignTaskAsync(AssignTaskDto taskAssignment)
    {
        
        var isAdded = await _repository.AssignTaskAsync(taskAssignment);
        return new Response<bool>(isAdded);
    }

    public async Task<Response<bool>> UpdateTaskAssignmentAsync(UpdateTaskAssignmentDto updateTask)
    {
        var isUpdated = await _repository.UpdateTaskAssignmentAsync(updateTask);
        return new Response<bool>(isUpdated);
    }

    public async Task<Response<GetTaskAssignmentDto>>? GetTaskAssignmentInfoAsync(int id)
    {
        var task = await _repository.GetTaskAssignmentInfoAsync(id)!;
        var dto = new GetTaskAssignmentDto
        {
            TaskAssignmentId = task.Id,
            User = new GetUserDto
            {
                Id = task.UserId, Name = task.User.Name, Email = task.User.Email,
                RegistrationDate = task.User.RegistrationDate
            },
            Task = new GetTaskDto
            {
                Id = task.TaskId,
                Title = task.Task.Title,
                Description = task.Task.Description,
                CreatedDate = task.Task.CreatedDate,
                CreatedBy = task.Task.CreatedUser.Name,
                DueDate = task.Task.DueDate,
                InProject = task.Task.Project.Name
            },
            AssignedDate = task.AssignedDate
        };
        
        return new Response<GetTaskAssignmentDto>(dto);
    }

    public async Task<Response<bool>> DeleteTaskAssignmentAsync(int id)
    {
        var isDeleted = await _repository.DeleteTaskAssignmentAsync(id);
        return new Response<bool>(isDeleted);
    }

    public async Task<Response<List<GetTaskAssignmentDto>>> GetTaskAssignmentByUserAsync(RequestTaskByUsernameDto dto)
    {
        var list = await _repository.GetTaskAssignmentByUserAsync(dto);
        var assignments = list.Select(task => new GetTaskAssignmentDto
        {
            TaskAssignmentId = task.Id,
            User = new GetUserDto
            {
                Id = task.UserId, Name = task.User.Name, Email = task.User.Email,
                RegistrationDate = task.User.RegistrationDate
            },
            Task = new GetTaskDto
            {
                Id = task.TaskId,
                Title = task.Task.Title,
                Description = task.Task.Description,
                CreatedDate = task.Task.CreatedDate,
                CreatedBy = task.Task.CreatedUser.Name,
                DueDate = task.Task.DueDate,
                InProject = task.Task.Project.Name
            },
            AssignedDate = task.AssignedDate
        }).ToList();

        return new Response<List<GetTaskAssignmentDto>>(assignments);
    }

    public async Task<Response<List<GetTaskAssignmentDto>>> GetTaskAssignmentByTaskAsync(RequestAssignmentByTaskDto dto)
    {
        var list = await _repository.GetTaskAssignmentByTaskAsync(dto);
        var assignments = list.Select(task => new GetTaskAssignmentDto
        {
            TaskAssignmentId = task.Id,
            User = new GetUserDto
            {
                Id = task.UserId, Name = task.User.Name, Email = task.User.Email,
                RegistrationDate = task.User.RegistrationDate
            },
            Task = new GetTaskDto
            {
                Id = task.TaskId,
                Title = task.Task.Title,
                Description = task.Task.Description,
                CreatedDate = task.Task.CreatedDate,
                CreatedBy = task.Task.CreatedUser.Name,
                DueDate = task.Task.DueDate,
                InProject = task.Task.Project.Name
            },
            AssignedDate = task.AssignedDate
        }).ToList();
        
        return new Response<List<GetTaskAssignmentDto>>(assignments);
    }
}