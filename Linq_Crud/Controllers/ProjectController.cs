using Clean.Application.Abstractions;
using Clean.Application.Dtos.Project;
using Microsoft.AspNetCore.Mvc;

namespace Linq_Crud.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : Controller
{
    private readonly IProjectService _service;

    public ProjectController(IProjectService service)
    {
        _service = service;
    }
    
    [HttpPost("add-project")]
    public async Task<IActionResult> AddProject(AddProjectDto dto)
    {
        var response = await _service.AddProjectAsync(dto);
        return Ok(response);
    }

    [HttpGet("get-all-projects")]
    public async Task<IActionResult> GetProjects()
    {
        var response = await _service.GetAllProjectsAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var response = await _service.GetProjectByIdAsync(id);
        return Ok(response);
    }

    [HttpPut("update-project")]
    public async Task<IActionResult> UpdateProject(UpdateProjectDto dto)
    {
        var response = await _service.UpdateProjectAsync(dto);
        return Ok(response);
    }

    [HttpDelete("delete-project")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var response = await _service.DeleteProjectAsync(id);
        return Ok(response);
    }

    [HttpPost("get-project-with-most-tasks")]
    public async Task<IActionResult> GetProjectWithMostTasks()
    {
        var response = await _service.GetProjectWithMostTaskAsync();
        return Ok(response);
    }

    [HttpGet("get-project-with-avg-duration")]
    public async Task<IActionResult> ProjectsWithAvgDurationInDaysAsync()
    {
        var response = await _service.GetProjectsWithAverageDurationAsync();
        return Ok(response);
    }

    [HttpGet("get-projects-with-progress")]
    public async Task<IActionResult> GetProjectsWithProgress()
    {
        var response = await _service.GetProjectWithProgressAsync();
        return Ok(response);
    }
}