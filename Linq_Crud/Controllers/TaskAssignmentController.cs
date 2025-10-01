using Clean.Application.Abstractions;
using Clean.Application.Dtos.Task;
using Clean.Application.Dtos.TaskAssignment;
using Microsoft.AspNetCore.Mvc;

namespace Linq_Crud.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskAssignmentController : Controller
{
    private readonly ITaskAssignmentService _service;

    public TaskAssignmentController(ITaskAssignmentService service)
    {
        _service = service;
    }

    [HttpPost("assign-task-assignment")]
    public async Task<IActionResult> AssignTaskAssignmentAsync(AssignTaskDto dto)
    {
        var response = await _service.AssignTaskAsync(dto);
        return Ok(response);
    }

    [HttpPut("update-task-assignment")]
    public async Task<IActionResult> UpdateTaskAssignmentAsync([FromQuery] UpdateTaskAssignmentDto dto)
    {
        var response = await _service.UpdateTaskAssignmentAsync(dto);
        return Ok(response);
    }

    [HttpGet("get-task-assignment")]
    public async Task<IActionResult> GetTaskAssignmentByIdAsync([FromQuery] int id)
    {
        var response = await _service.GetTaskAssignmentInfoAsync(id)!;
        return Ok(response);
    }

    [HttpDelete("delete-assignment")]
    public async Task<IActionResult> DeleteTaskAssignmentAsync(int id)
    {
        var response = await _service.DeleteTaskAssignmentAsync(id);
        return Ok(response);
    }

    [HttpPost("get-task-assignment-by-user")]
    public async Task<IActionResult> GetTaskAssignmentByUserAsync([FromQuery]RequestTaskByUsernameDto dto)
    {
        var response = await _service.GetTaskAssignmentByUserAsync(dto);
        return Ok(response);
    }

    [HttpPost("get-task-assignment-by-task")]
    public async Task<IActionResult> GetTaskAssignmentByTaskAsync([FromQuery]RequestAssignmentByTaskDto dto)
    {
        var response = await _service.GetTaskAssignmentByTaskAsync(dto);
        return Ok(response);
    }
}