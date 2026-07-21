using Microsoft.EntityFrameworkCore;
using WebApiTaskTracker.Data.Entities;

namespace WebApiTaskTracker.Data.Databases
{
    public class TaskTrackerDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}