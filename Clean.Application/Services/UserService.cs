using Clean.Application.Abstractions;
using Clean.Application.Dtos.User;
using Clean.Domain.Entities;

namespace Clean.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<Response<bool>> AddUserAsync(AddUserDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email
        };
        var isAdded = await _repository.AddUserAsync(user);
        return new Response<bool> (isAdded);
    }

    public async Task<Response<List<GetUserDto>>> GetUsersAsync()
    {
        var list = await _repository.GetUsersAsync();
        var dtos = new List<GetUserDto>();
        list.ForEach(u=> dtos.Add(new GetUserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            RegistrationDate = u.RegistrationDate
        }));

        return new Response<List<GetUserDto>>(dtos);
    }

    public async Task<Response<GetUserDto>>? GetUserByIdAsync(int id)
    {
        var user = await _repository.GetUserByIdAsync(id)!;
        if (user is null)
        {
            throw new ArgumentNullException($"No user with the id: {id}");
        }

        var userDto = new GetUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            RegistrationDate = user.RegistrationDate
        };

        return new Response<GetUserDto>(userDto);
    }

    public async Task<Response<bool>> UpdateUserAsync(UpdateUserDto dto)
    {
        var user = new User
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
        };

        var isUpdated = await _repository.UpdateUserAsync(user);

        return new Response<bool>(isUpdated);
    }

    public async Task<Response<bool>> DeleteUserAsync(int id)
    {
        var isDeleted = await _repository.DeleteUserAsync(id);
        return new Response<bool>(isDeleted);
    }

    public async Task<Response<UserTaskCountDto>> GetUserWithMostTasksAsync()
    {
        var user = await _repository.GetUserWithMostTasksAsync()!;
        if (user is null)
        {
            throw new ArgumentNullException();
        }

        var userDto = new UserTaskCountDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            RegistrationDate = user.RegistrationDate,
            TaskCount = user.Assignments.Count
        };

        return new Response<UserTaskCountDto>(userDto);
    }

    public async Task<Response<List<UserTaskCountDto>>> GetUsersWithoutTasksAsync()
    {
        var list = await _repository.GetUsersWithoutTasksAsync();
        return new Response<List<UserTaskCountDto>>(list);
    }

    public async Task<Response<List<UserTaskCountDto>>> GetTopFiveActiveUsersAsync()
    {
        var list = await _repository.GetTopFiveActiveUsersAsync();
        return new Response<List<UserTaskCountDto>>(list);
    }

    public async Task<Response<List<GetUserWithTasksDto>>> GetNewUsersWithTasks()
    {
        var list = await _repository.GetNewUsersWithTasks();
        return new Response<List<GetUserWithTasksDto>>(list);
    }

    public async Task<Response<List<GetUserWithTasksDto>>> GetUserWithOverdueTasks()
    {
        var list = await _repository.GetUserWithOverdueTasks();
        return new Response<List<GetUserWithTasksDto>>(list);
    }
}