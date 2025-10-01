using Clean.Application.Dtos.Project;
using Clean.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Clean.Application.Abstractions;

public interface IProjectRepository
{

    public Task<bool> AddProjectAsync(Project project);
    public Task<List<Project>> GetAllProjectsAsync();
    public Task<Project> GetProjectByIdAsync(int id);
    public Task<bool> UpdateProjectAsync(Project project);
    public Task<bool> DeleteProjectAsync(int id);

    public Task<Project> GetProjectWithMostTasksAsync();
    public Task<GetProjectDto>? GetProjectByNameAsync(GetProjectByName dto);

    public Task<List<GetProjectWithAvDurationDto>> GetProjectsWithAverageDurationAsync();

    public Task<List<GetProjectWithProgressDto>> GetProjectWithProgressAsync();
}