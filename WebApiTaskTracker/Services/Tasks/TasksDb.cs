using System.Collections.Concurrent;
using WebApiTaskTracker.Models;

namespace WebApiTaskTracker.Services.Tasks
{
    public class TasksDb
    {
        private readonly ConcurrentDictionary<int, TaskItem> _tasks = new();

        public TaskItem? this[int id]
        {
            get => _tasks.TryGetValue(id, out var task) ? task : null;
            set
            {
                if (value != null)
                {
                    _tasks[id] = value;
                }
            }
        }

        public IEnumerable<TaskItem> GetAll() => _tasks.Values;

        public bool Remove(int id) => _tasks.TryRemove(id, out _);
    }
}