using WebApiTaskTracker.Data.Entities;

namespace WebApiTaskTracker.DTOs.Tasks
{
    public record TaskSummaryResponse(
        Guid Id,
        string Title,
        string? Category,
        DateTime? DueDate,
        int Priority
    )
    {
        public static TaskSummaryResponse FromEntity(TaskEntity entity)
        {
            return new TaskSummaryResponse(
                Id: entity.Id,
                Title: entity.Title,
                Category: entity.Category,
                DueDate: entity.DueDate,
                Priority: entity.Priority
            );
        }
    }
}
