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
            var task = _db[id];
            if (task == null)
                return Task.FromResult<TaskResponse?>(null);

            return Task.FromResult(new TaskResponse(task.Id, task.Title, task.Description, task.Category, task.DueDate, task.Priority));
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
            var existingTask = _db[id];
            if (existingTask == null)
                return Task.CompletedTask;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Category = task.Category;
            existingTask.DueDate = task.DueDate;
            existingTask.Priority = task.Priority;

            _db[id] = existingTask;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _db.Remove(id);
            return Task.CompletedTask;
        }
    }
}
