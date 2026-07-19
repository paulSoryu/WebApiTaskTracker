namespace WebApiTaskTracker.DTOs.Tasks
{
    public record UpdateTaskRequest(
        string Title,
        string Description,
        string Category,
        DateTime DueDate,
        int Priority
    )
    {
    }
}
