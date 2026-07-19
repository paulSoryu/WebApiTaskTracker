using WebApiTaskTracker.DTOs.Tasks;

namespace WebApiTaskTracker.Services.Tasks
{
    public interface ITaskService
    {
        Task<TaskResponse?> GetByIdAsync(int id);
        Task<IEnumerable<TaskSummaryResponse>> GetAllAsync();
        Task<TaskResponse> CreateAsync(CreateTaskRequest task);
        Task UpdateAsync(int id, UpdateTaskRequest task);
        Task DeleteAsync(int id);
    }
}
