using WebApiTaskTracker.Data.Entities;

// this is a DTO for returning a summary of task information, including a method to convert a TaskEntity to this DTO
// this breaks the single responsibility principle, as the DTO is responsible for both data transfer and conversion, but it is convenient for this simple app
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
