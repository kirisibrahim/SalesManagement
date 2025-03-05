using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Validators
{
    public class SupplierValidator : AbstractValidator<SupplierDto>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tedarikçi adı boş olamaz")
                .MaximumLength(100).WithMessage("Tedarikçi adı 100 karakteri geçemez");

            RuleFor(x => x.ContactInfo)
                .NotEmpty().WithMessage("İletişim Bilgileri boş bırakılamaz")
                .MaximumLength(200).WithMessage("İletişim Bilgileri 200 karakteri geçemez");
        }
    }
}
