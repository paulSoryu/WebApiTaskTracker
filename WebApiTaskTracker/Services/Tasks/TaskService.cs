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
        var task = await _db.Tasks
            .AsNoTracking()
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
            throw new EntityNotFoundException($"Task {id} not found.");

        return TaskResponse.FromEntity(task);
    }

    public async Task<IEnumerable<TaskSummaryResponse>> GetAllAsync()
    {
        var result = await _db.Tasks
            .AsNoTracking()
            .Include(t => t.Category)
            .Select(p => TaskSummaryResponse.FromEntity(p))
            .ToListAsync();

        return result;
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest dto, Guid userId)
    {
        var existingCategory = await GetOrCreateCategoryAsync(dto.CategoryName!, userId);
        Guid? categoryId = existingCategory?.Id;
        var entity = dto.ToEntity(categoryId, userId);

        _db.Tasks.Add(entity);
        await _db.SaveChangesAsync();

        return TaskResponse.FromEntity(entity);
    }

    public async Task UpdateAsync(Guid taskId, UpdateTaskRequest dto)
    {
        var existingTask = await _db.Tasks.FindAsync(taskId);
        if (existingTask == null)
            throw new EntityNotFoundException($"Task {taskId} not found.");

        var existingCategory = await GetOrCreateCategoryAsync(dto.CategoryName!, existingTask.UserId);
        Guid? categoryId = existingCategory?.Id;
        

        dto.UpdateEntity(existingTask, categoryId);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid taskId)
    {
        var existingTask = await _db.Tasks.FindAsync(taskId);
        if (existingTask == null)
            throw new EntityNotFoundException($"Task {taskId} not found.");

        _db.Remove(existingTask);
        await _db.SaveChangesAsync();
    }

    private async Task<CategoryEntity?> GetOrCreateCategoryAsync(string categoryName, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
            return null;

        var normalizedName = categoryName.Trim().ToLower();
        var category = await _db.Categories
            .FirstOrDefaultAsync(c => c.Title.ToLower() == normalizedName);

        if (category == null)
        {
            category = new CategoryEntity
            {
                Id = Guid.NewGuid(),
                Title = categoryName.Trim(),
                UserId = userId
            };
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }
        return category;
    }
}
