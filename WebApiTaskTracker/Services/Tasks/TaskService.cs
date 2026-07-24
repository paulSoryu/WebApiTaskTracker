using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiTaskTracker.Data.Databases;
using WebApiTaskTracker.Data.Entities;
using WebApiTaskTracker.DTOs.Tasks;
using WebApiTaskTracker.Utilities;

namespace WebApiTaskTracker.Services.Tasks;

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

        return TaskResponse.FromEntity(task);
    }

    public async Task<IEnumerable<TaskSummaryResponse>> GetAllAsync()
    {
        var result = await _db.Tasks
            .Select(p => TaskSummaryResponse.FromEntity(p))
            .ToListAsync();

        return result;
    }

    // Bug with categoryEntity trying to write into categories List
    public async Task<TaskResponse> CreateAsync(CreateTaskRequest task, Guid userId)
    {
        CategoryEntity? category = null;

        if (!string.IsNullOrWhiteSpace(task.Category))
        {
            var normalizedName = task.Category.Trim();

            category = await _db.Categories.FirstOrDefaultAsync(c => c.Title.ToLower() == normalizedName.ToLower());

            if (category == null)
            {
                category = new CategoryEntity { Title = normalizedName, UserId = userId};
                _db.Categories.Add(category); 
            }
        }

        var entity = task.ToEntity(category, userId);
        _db.Tasks.Add(entity);

        await _db.SaveChangesAsync();
        return TaskResponse.FromEntity(entity);
    }

    public async Task UpdateAsync(Guid id, UpdateTaskRequest task)
    {
        var existingTask = await _db.Tasks.FindAsync(id);
        if (existingTask == null)
            throw new EntityNotFoundException($"Task {id} not found.");

        task.UpdateEntity(existingTask);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var existingTask = await _db.Tasks.FindAsync(id);
        if (existingTask == null)
            throw new EntityNotFoundException($"Task {id} not found.");

        _db.Remove(existingTask);
        await _db.SaveChangesAsync();
    }
}
