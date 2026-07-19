namespace WebApiTaskTracker.DTOs.Tasks
{
    public record TaskSummaryResponse(
        int Id,
        string Title,
        string Category,
        DateTime DueDate,
        int Priority
    )
    {
    }
}
