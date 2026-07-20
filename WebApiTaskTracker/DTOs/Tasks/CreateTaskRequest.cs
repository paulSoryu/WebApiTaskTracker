using System;
using FluentValidation;
using WebApiTaskTracker.Data.Entities;

namespace WebApiTaskTracker.DTOs.Tasks
{
    public record CreateTaskRequest(
        string Title,
        string Description,
        string Category,
        DateTime DueDate,
        int Priority
    )
    {
        public TaskEntity ToEntity()
        {
            return new TaskEntity
            {
                Title = this.Title,
                Description = this.Description,
                Category = this.Category,
                DueDate = this.DueDate,
                Priority = this.Priority,
                CreatedAt = DateTime.UtcNow
            };
        }

        public class Validator : AbstractValidator<CreateTaskRequest>
        {
            public Validator()
            {
                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Title cannot be empty.")
                    .Length(3, 50).WithMessage("Title must be between 3 and 50 characters.");

                RuleFor(x => x.Description)
                    .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

                RuleFor(x => x.Category)
                    .NotEmpty().WithMessage("Category cannot be empty.")
                    .Length(1, 20).WithMessage("Category must be between 1 and 20 characters.");

                RuleFor(x => x.DueDate)
                    .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Due date must be today or in the future.");

                RuleFor(x => x.Priority)
                    .InclusiveBetween(1, 5).WithMessage("Priority must be between 1 and 5.");
            }
        }
    }

    
}

