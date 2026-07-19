using WebApiTaskTracker.DTOs.Tasks;

namespace WebApiTaskTracker.Services.Tasks
{
    public interface ITaskService
    {
        Task<TaskResponse?> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskSummaryResponse>> GetAllTasksAsync();
        Task<TaskResponse> CreateTaskAsync(CreateTaskRequest task);
        Task UpdateTaskAsync(int id, UpdateTaskRequest task);
        Task DeleteTaskAsync(int id);
    }
}
