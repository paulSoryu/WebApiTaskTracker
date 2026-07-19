using Scalar.AspNetCore;
using WebApiTaskTracker.Endpoints;
using WebApiTaskTracker.Services.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITaskService, TaskService>();

// Access by adding /scalar to the base URL of the API. For example, https://localhost:5001/scalar
builder.Services.AddOpenApi();

// Access by adding /swagger to the base URL of the API. For example, https://localhost:5001/swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapTaskEndpoints();

app.Run();