using Clean.Application.Dtos.User;
using Clean.Domain.Entities;

namespace Clean.Application.Abstractions;

public interface IUserRepository
{
    public Task<bool> AddUserAsync(User user);
    public Task<List<User>> GetUsersAsync();
    public Task<User>? GetUserByIdAsync(int id);

    public Task<User>? GetUserByNameAsync(string name);
    public Task<bool> UpdateUserAsync(User dto);
    public Task<bool> DeleteUserAsync(int id);
    public Task<User>? GetUserWithMostTasksAsync();
    
    public Task<List<UserTaskCountDto>> GetUsersWithoutTasksAsync();

    public Task<List<UserTaskCountDto>> GetTopFiveActiveUsersAsync();

    public Task<List<GetUserWithTasksDto>> GetNewUsersWithTasks();

    public Task<List<GetUserWithTasksDto>> GetUserWithOverdueTasks();
}