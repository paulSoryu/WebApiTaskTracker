using WebApiTaskTracker.Data.Entities;

namespace WebApiTaskTracker.DTOs.Tasks
{
    public record TaskResponse(
        int Id,
        string Title,
        string? Description,
        string? Category,
        DateTime? DueDate,
        int Priority
    )
    {
        public static TaskResponse FromEntity(TaskEntity entity)
        {
            return new TaskResponse(
                Id: entity.Id,
                Title: entity.Title,
                Description: entity.Description,
                Category: entity.Category,
                DueDate: entity.DueDate,
                Priority: entity.Priority
            );
        }
    }
}
