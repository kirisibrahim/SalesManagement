using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı ismi boş olamaz.")
                .MaximumLength(20).WithMessage("Kullanıcı adı en fazla 20 karakter olabilir.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("Geçerli bir rol seçilmelidir.");
        }
    }
}
