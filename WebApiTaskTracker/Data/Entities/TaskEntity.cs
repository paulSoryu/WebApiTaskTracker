namespace WebApiTaskTracker.Data.Entities;

public class TaskEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }


    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    
    public Guid? CategoryId { get; set; } 
    public CategoryEntity? Category { get; set; }
}