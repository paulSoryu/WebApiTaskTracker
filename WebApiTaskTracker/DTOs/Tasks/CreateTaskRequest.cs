namespace WebApiTaskTracker.DTOs.Tasks
{
    public record CreateTaskRequest(
        string Title,
        string Description,
        string Category,
        DateTime DueDate,
        int Priority
    )
    {
    }
}
