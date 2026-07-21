using Microsoft.EntityFrameworkCore;
using WebApiTaskTracker.Data.Entities;

namespace WebApiTaskTracker.Data {
    public class TaskTrackerDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
        public TaskTrackerDbContext() => Database.EnsureCreated();

        public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=data/TaskTracker.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}