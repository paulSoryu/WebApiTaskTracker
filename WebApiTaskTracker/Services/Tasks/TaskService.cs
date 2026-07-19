using WebApiTaskTracker.DTOs.Tasks;

namespace WebApiTaskTracker.Services.Tasks
{
    public class TaskService : ITaskService
    {
        public Task<TaskResponse?> GetTaskByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskSummaryResponse>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TaskResponse> CreateTaskAsync(CreateTaskRequest task)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTaskAsync(int id, UpdateTaskRequest task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
