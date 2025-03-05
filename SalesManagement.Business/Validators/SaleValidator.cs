using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Validators
{
    public class SaleValidator : AbstractValidator<SaleDto>
    {
        public SaleValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId 0'dan büyük olmalıdır.");
            RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır.");
            RuleFor(x => x.SaleDate).NotEmpty().WithMessage("SaleDate boş olamaz.");
        }
    }
}
