using Clean.Application.Abstractions;
using Clean.Application.Dtos.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Linq_Crud.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : Controller
{
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
        _service = service;
    }

    [HttpPost("add-task")]
    public async Task<IActionResult> AddTaskAsync(AddTaskDto dto)
    {
        var response = await _service.AddTaskAsync(dto);
        return Ok(response);
    }

    [HttpGet("get-all-tasks")]
    public async Task<IActionResult> GetAllTasksAsync()
    {
        var response = await _service.GetTasksAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskByIdAsync(int id)
    {
        var response = await _service.GetTaskByIdAsync(id)!;
        return Ok(response);
    }

    [HttpPut("update-task")]
    public async Task<IActionResult> UpdateTaskAsync(UpdateTaskDto dto)
    {
        var response = await _service.UpdateTaskAsync(dto);
        return Ok(response);
    }

    [HttpDelete("delete-task")]
    public async Task<IActionResult> DeleteTaskAsync(int id)
    {
        var response = await _service.DeleteTaskAsync(id);
        return Ok(response);
    }
    
    [HttpPost("get-by-project")]
    public async Task<IActionResult> GetTasksByProjectAsync([FromQuery]RequestTasksByProjectDto dto)
    {
        var response = await _service.GetTaskByProjectAsync(dto);
        return Ok(response);
    }

    [HttpPost("get-by-username")]
    public async Task<IActionResult> GetTasksByUsernameAsync([FromQuery] RequestTaskByUsernameDto dto)
    {
        var response = await _service.GetTasksByUserAsync(dto);
        return Ok(response);
    }

    [HttpPost("get-tasks-due-soon")]
    public async Task<IActionResult> GetTasksDueSoonAsync([FromQuery] int days)
    {
        var resposne = await _service.GetTasksDueSoon(days);
        return Ok(resposne);
    }

    [HttpGet("get-overdue-tasks")]
    public async Task<IActionResult> GetOverdueTasksAsync()
    {
        var response = await _service.GetOverdueTasksAsync();
        return Ok(response);
    }

    [HttpGet("get-tasks-by-month")]
    public async Task<IActionResult> GetTasksByMonthAsync()
    {
        var response = await _service.GetTasksByMonthsAsync();
        return Ok(response);
    }

    [HttpGet("get-multi-assigned-tasks")]
    public async Task<IActionResult> GetMultiAssignedTasksAsync()
    {
        var response = await _service.GetMultiAssignedTasksAsync();
        return Ok(response);
    }
}