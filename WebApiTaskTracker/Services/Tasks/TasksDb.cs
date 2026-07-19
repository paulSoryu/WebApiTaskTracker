using System.Collections.Concurrent;
using WebApiTaskTracker.Data;

namespace WebApiTaskTracker.Services.Tasks
{
    public class TasksDb
    {
        private readonly ConcurrentDictionary<int, TaskItem> _tasks = new();
        
        private int _nextId = 0;

        public TaskItem Add(TaskItem task)
        {
            task.Id = Interlocked.Increment(ref _nextId);
            
            _tasks.TryAdd(task.Id, task);
            
            return task;
        }

        public TaskItem? this[int id]
        {
            get => _tasks.TryGetValue(id, out var task) ? task : null;
            set { if (value != null) _tasks[id] = value; }
        }

        public IEnumerable<TaskItem> GetAll() => _tasks.Values;
        public bool Remove(int id) => _tasks.TryRemove(id, out _);
    }
}