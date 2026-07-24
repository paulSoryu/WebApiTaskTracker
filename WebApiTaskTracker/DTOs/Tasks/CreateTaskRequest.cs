using System;
using FluentValidation;
using WebApiTaskTracker.Data.Entities;

// this is a DTO for creating a task, including validation rules and a method to convert the DTO to a TaskEntity
// this breaks the single responsibility principle, as the DTO is responsible for data transfer, conversion and validation, but it is convenient for this simple app
namespace WebApiTaskTracker.DTOs.Tasks;

public record CreateTaskRequest(
    string Title,
    string Description,
    string Category,
    DateTime DueDate,
    int Priority
)
{
    public TaskEntity ToEntity(CategoryEntity? category, Guid userId)
    {
        return new TaskEntity
        {
            Title = this.Title,
            Description = this.Description,
            DueDate = this.DueDate,
            Priority = this.Priority,
            CreatedAt = DateTime.UtcNow,
            Category = category,
            UserId = userId
        };
    }

    public class Validator : AbstractValidator<CreateTaskRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters.")
                .MaximumLength(20).WithMessage("Title must be at most 20 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description must be at most 2000 characters.");

            RuleFor(x => x.DueDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Due date must be today or in the future.");

            RuleFor(x => x.Priority)
                .InclusiveBetween(1, 5).WithMessage("Priority must be between 1 and 5.");
        }
    }
}



