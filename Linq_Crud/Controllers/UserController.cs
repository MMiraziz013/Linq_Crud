using Clean.Application.Abstractions;
using Clean.Application.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace Linq_Crud.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("add-user")]
    public async Task<IActionResult> AddUserAsync(AddUserDto dto)
    {
        var response = await _service.AddUserAsync(dto);
        return Ok(response);
    }

    [HttpGet("get-all-users")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var response = await _service.GetUsersAsync();
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var response = await _service.GetUserByIdAsync(id);
        return Ok(response);
    }

    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUserAsync(UpdateUserDto dto)
    {
        var response = await _service.UpdateUserAsync(dto);
        return Ok(response);
    }

    [HttpDelete("delete-user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var response = await _service.DeleteUserAsync(id);
        return Ok(response);
    }

    [HttpGet("get-user-with-most-tasks")]
    public async Task<IActionResult> GetUserWithMostTasksAsync()
    {
        var response = await _service.GetUserWithMostTasksAsync();
        return Ok(response);
    }

    [HttpGet("get-users-without-tasks")]
    public async Task<IActionResult> GetUsersWithoutTasksAsync()
    {
        var response = await _service.GetUsersWithoutTasksAsync();
        return Ok(response);
    }

    [HttpGet("get-top-five-active-users")]
    public async Task<IActionResult> GetTopFiveActiveUsersAsync()
    {
        var response = await _service.GetTopFiveActiveUsersAsync();
        return Ok(response);
    }

    [HttpGet("get-new-students-with-tasks")]
    public async Task<IActionResult> GetNewStudentsWithTasksAsync()
    {
        var response = await _service.GetNewUsersWithTasks();
        return Ok(response);
    }

    [HttpGet("get-users-with-overdue-tasks")]
    public async Task<IActionResult> GetUsersWithOverdueTasksAsync()
    {
        var response = await _service.GetUserWithOverdueTasks();
        return Ok(response);
    }

}