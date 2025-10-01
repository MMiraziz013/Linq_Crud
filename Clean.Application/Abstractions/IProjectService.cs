using Clean.Application.Dtos.Project;
using Clean.Application.Services;
using Clean.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Clean.Application.Abstractions;

public interface IProjectService
{
    public Task<Response<bool>> AddProjectAsync(AddProjectDto project);
    public Task<Response<List<GetAllProjectsDto>>> GetAllProjectsAsync();
    public Task<Response<GetAllProjectsDto>> GetProjectByIdAsync(int id);
    public Task<Response<bool>> UpdateProjectAsync(UpdateProjectDto project);
    public Task<Response<bool>> DeleteProjectAsync(int id);

    public Task<Response<GetAllProjectsDto>> GetProjectWithMostTaskAsync();
    public Task<Response<GetAllProjectsDto>>? GetProjectByNameAsync(GetProjectByName dto);
    public Task<Response<List<GetProjectWithAvDurationDto>>> GetProjectsWithAverageDurationAsync();
    
    public Task<Response<List<GetProjectWithProgressDto>>> GetProjectWithProgressAsync();
    
}