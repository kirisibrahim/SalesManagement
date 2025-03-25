using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Validators
{
    public class UpdateTasksTaskValidator : AbstractValidator<UpdateTasksTaskDto>
    {
        public UpdateTasksTaskValidator()
        {
            RuleFor(x => x.Title)
                 .NotEmpty().WithMessage("Başlık booş olamaz.")
                 .Length(3, 100).WithMessage("Başlıkm 3 ila 100 karakter arasında olmalı.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.")
                .Length(10, 500).WithMessage("Açıklama 5 ila 500 karakter arasında olmalı.");

            RuleFor(x => x.Durum)
                .IsInEnum().WithMessage("Geçersiz durum");

            RuleFor(x => x.UserIds)
                .NotEmpty().WithMessage("Göreve en az bir kullanıcı atanmalıdır.")
                .Must(userIds => userIds.All(id => id > 0)).WithMessage("User Id 0 dan büyük olmalı.");
        }
    }
}
