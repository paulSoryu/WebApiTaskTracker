using Microsoft.AspNetCore.Identity;

namespace WebApiTaskTracker.Data.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public List<CategoryEntity> Categories { get; set; } = [];
    public List<TaskEntity> Tasks { get; set; } = [];
}
