using WebApiTaskTracker.DTOs.Tasks;
using WebApiTaskTracker.Data;

namespace WebApiTaskTracker.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly TasksDb _db;

        public TaskService(TasksDb db)
        {
            _db = db;
        }

        public Task<TaskResponse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskSummaryResponse>> GetAllAsync()
        {
            var result = _db.GetAll()
                .Select(p => new TaskSummaryResponse(
                    p.Id,
                    p.Title,
                    p.Category,
                    p.DueDate,
                    p.Priority))
                .ToList(); // materialize to avoid deferred-execution issues

            return Task.FromResult<IEnumerable<TaskSummaryResponse>>(result);
        }

        public Task<TaskResponse> CreateAsync(CreateTaskRequest task)
        {
            var result = _db.Add(new TaskItem
            {
                Title = task.Title,
                Description = task.Description,
                Category = task.Category,
                DueDate = task.DueDate,
                Priority = task.Priority
            });
            return Task.FromResult(new TaskResponse(result.Id, result.Title, result.Description, result.Category, result.DueDate, result.Priority));
        }

        public Task UpdateAsync(int id, UpdateTaskRequest task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
