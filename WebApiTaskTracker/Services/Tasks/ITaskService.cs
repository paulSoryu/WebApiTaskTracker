using WebApiTaskTracker.DTOs.Tasks;

namespace WebApiTaskTracker.Services.Tasks;

public interface ITaskService
{
    Task<TaskResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<TaskSummaryResponse>> GetAllAsync();
    Task<TaskResponse> CreateAsync(CreateTaskRequest task, Guid userId);
    Task UpdateAsync(Guid id, UpdateTaskRequest task);
    Task DeleteAsync(Guid id);
}
