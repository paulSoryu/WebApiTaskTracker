using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTaskTracker.Data.Databases;
using WebApiTaskTracker.DTOs.Tasks;

namespace WebApiTaskTracker.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly TaskTrackerDbContext _db;

        public TaskService(TaskTrackerDbContext db)
        {
            _db = db;
        }

        public async Task<TaskResponse?> GetByIdAsync(Guid id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null)
                return null;

            return new TaskResponse(task.Id, task.Title, task.Description, task.Category, task.DueDate, task.Priority);
        }

        public async Task<IEnumerable<TaskSummaryResponse>> GetAllAsync()
        {
            var result = await _db.Tasks
                .Select(p => new TaskSummaryResponse(
                    p.Id,
                    p.Title,
                    p.Category,
                    p.DueDate,
                    p.Priority))
                .ToListAsync();

            return result;
        }

        public async Task<TaskResponse> CreateAsync(CreateTaskRequest task)
        {
            var entity = task.ToEntity();
            var entry = await _db.AddAsync(entity);
            await _db.SaveChangesAsync();

            var result = entry.Entity;
            return new TaskResponse(result.Id, result.Title, result.Description, result.Category, result.DueDate, result.Priority);
        }

        public async Task UpdateAsync(Guid id, UpdateTaskRequest task)
        {
            var existingTask = await _db.Tasks.FindAsync(id);
            if (existingTask == null)
                return;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Category = task.Category;
            existingTask.DueDate = task.DueDate;
            existingTask.Priority = task.Priority;

            _db.Update(existingTask);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingTask = await _db.Tasks.FindAsync(id);
            if (existingTask == null)
                return;

            _db.Remove(existingTask);
            await _db.SaveChangesAsync();
        }
    }
}
