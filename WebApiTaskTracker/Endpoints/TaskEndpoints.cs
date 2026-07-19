using WebApiTaskTracker.DTOs.Tasks;
using WebApiTaskTracker.Services.Tasks;

namespace WebApiTaskTracker.Endpoints
{
    public static class TaskEndpoints
    {
        public static void MapTaskEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var routeGroup = endpoints.MapGroup("/api/tasks");

            routeGroup.MapGet("/", GetAllTasks);
            routeGroup.MapGet("/{id:int}", GetTaskById).WithName("GetTaskById");
            routeGroup.MapPost("/", CreateTask);
            routeGroup.MapPut("/{id:int}", UpdateTask);
            routeGroup.MapDelete("/{id:int}", DeleteTask);
        }

        private static async Task<IResult> GetAllTasks(ITaskService taskService)
        {
            // Implement logic to retrieve all tasks from the database or any other data source
            var tasks = await taskService.GetAllTasksAsync();
            return Results.Ok(tasks);
        }

        private static async Task<IResult> GetTaskById(int id, ITaskService taskService)
        {
            // Implement logic to retrieve a task by its ID from the database or any other data source
            var task = await taskService.GetTaskByIdAsync(id);
            return Results.Ok(task);
        }

        private static async Task<IResult> CreateTask(CreateTaskRequest taskRequest, ITaskService taskService)
        {
            // Implement logic to create a new task in the database or any other data source
            TaskResponse createdTask = await taskService.CreateTaskAsync(taskRequest);
            return Results.CreatedAtRoute("GetTaskById", new { id = createdTask.Id }, createdTask);
        }

        private static async Task<IResult> UpdateTask(int id, UpdateTaskRequest taskRequest, ITaskService taskService)
        {
            // Implement logic to update an existing task in the database or any other data source
            await taskService.UpdateTaskAsync(id, taskRequest);
            return Results.NoContent();
        }

        private static async Task<IResult> DeleteTask(int id, ITaskService taskService)
        {
            // Implement logic to delete a task by its ID from the database or any other data source
            await taskService.DeleteTaskAsync(id);
            return Results.NoContent();
        }
    }
}
