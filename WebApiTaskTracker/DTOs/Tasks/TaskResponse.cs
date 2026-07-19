namespace WebApiTaskTracker.DTOs.Tasks
{
    public record TaskResponse(
        int Id,
        string Title,
        string Description,
        string Category,
        DateTime DueDate,
        int Priority
    )
    {
    }
}
