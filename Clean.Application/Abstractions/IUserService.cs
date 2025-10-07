using Clean.Application.Dtos.User;
using Clean.Application.Services;

namespace Clean.Application.Abstractions;

public interface IUserService
{
    public Task<Response<bool>> RegisterUserAsync(AddUserDto dto);
    public Task<Response<List<GetUserDto>>> GetUsersAsync();
    public Task<Response<GetUserDto>>? GetUserByIdAsync(int id);
    public Task<Response<bool>> UpdateUserAsync(UpdateUserDto dto);
    public Task<Response<bool>> DeleteUserAsync(int id);
    public Task<Response<UserTaskCountDto>> GetUserWithMostTasksAsync();

    public Task<Response<List<UserTaskCountDto>>> GetUsersWithoutTasksAsync();

    public Task<Response<List<UserTaskCountDto>>> GetTopFiveActiveUsersAsync();

    public Task<Response<List<GetUserWithTasksDto>>> GetNewUsersWithTasks();

    public Task<Response<List<GetUserWithTasksDto>>> GetUserWithOverdueTasks();

    Task<Response<string>> LoginAsync(UserLoginDto login);

}