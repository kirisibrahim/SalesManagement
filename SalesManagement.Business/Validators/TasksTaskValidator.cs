using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Validators
{
    public class TasksTaskValidator : AbstractValidator<TasksTaskDto>
    {
        public TasksTaskValidator()
        {
            RuleFor(x => x.Title)
                        .NotEmpty().WithMessage("Title is required.")
                        .Length(3, 100).WithMessage("Title must be between 3 and 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(10, 500).WithMessage("Description must be between 10 and 500 characters.");

            RuleFor(x => x.Durum)
                .IsInEnum().WithMessage("Invalid status.");

            RuleFor(x => x.UserIds)
                .NotEmpty().WithMessage("At least one user must be assigned to the task.")
                .Must(userIds => userIds.All(id => id > 0)).WithMessage("User IDs must be greater than 0.");
        }
    }
}
