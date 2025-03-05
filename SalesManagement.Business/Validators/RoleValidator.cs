using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Validators
{
    public class RoleValidator : AbstractValidator<RoleDto>
    {
        public RoleValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Role adı boş olamaz.")
                .Length(3, 30).WithMessage("Role adı 3 ile 30 karakter arasında olmalıdır.");
        }
    }
}
