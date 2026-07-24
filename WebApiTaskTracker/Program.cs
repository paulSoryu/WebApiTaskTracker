using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApiTaskTracker.Data.Databases;
using WebApiTaskTracker.Data.Entities;
using WebApiTaskTracker.Endpoints;
using WebApiTaskTracker.Services.Categories;
using WebApiTaskTracker.Services.Emails;
using WebApiTaskTracker.Services.Tasks;
using WebApiTaskTracker.Services.Users;
using WebApiTaskTracker.Utilities;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<UserEntity>(options => {
     options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<TaskTrackerDbContext>();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IEmailSender<UserEntity>, EmailSenderService>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddDbContext<TaskTrackerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Access by adding /scalar to the base URL of the API. For example, https://localhost:5001/scalar
//builder.Services.AddOpenApi();

// Access by adding /swagger to the base URL of the API. For example, https://localhost:5001/swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationRulesToSwagger();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TaskTrackerDbContext>();
    context.Database.Migrate();
}

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    //app.MapScalarApiReference();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapIdentityApi<UserEntity>();

// app.MapUserEndpoints();
app.MapTaskEndpoints();
// app.MapCategoryEndpoints();

app.MapPost("/logout", async (
    ClaimsPrincipal userPrincipal,
    UserManager<UserEntity> userManager) =>
{
    var user = await userManager.GetUserAsync(userPrincipal);
    if (user == null) return Results.Unauthorized();

    await userManager.RemoveAuthenticationTokenAsync(
        user,
        "[AspNetCoreIdentityBearerToken]",
        "refresh_token");

    return Results.Ok(new { message = "Logout succesful. Refresh token is deleted." });
})
.RequireAuthorization();

app.Run();