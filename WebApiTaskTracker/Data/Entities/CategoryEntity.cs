using System.Drawing;

namespace WebApiTaskTracker.Data.Entities;

public class CategoryEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Color Colour { get; set; } = Color.White;


    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;


    public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}
