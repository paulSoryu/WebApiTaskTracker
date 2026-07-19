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
            var tasks = await taskService.GetAllAsync();
            return Results.Ok(tasks);
        }

        private static async Task<IResult> GetTaskById(int id, ITaskService taskService)
        {
            var task = await taskService.GetByIdAsync(id);
            return Results.Ok(task);
        }

        private static async Task<IResult> CreateTask(CreateTaskRequest taskRequest, ITaskService taskService)
        {
            TaskResponse createdTask = await taskService.CreateAsync(taskRequest);
            return Results.CreatedAtRoute("GetTaskById", new { id = createdTask.Id }, createdTask);
        }

        private static async Task<IResult> UpdateTask(int id, UpdateTaskRequest taskRequest, ITaskService taskService)
        {
            await taskService.UpdateAsync(id, taskRequest);
            return Results.NoContent();
        }

        private static async Task<IResult> DeleteTask(int id, ITaskService taskService)
        {
            await taskService.DeleteAsync(id);
            return Results.NoContent();
        }
    }
}
