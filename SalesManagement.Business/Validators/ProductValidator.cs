using FluentValidation;
using SalesManagement.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Business.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ürün adı zorunludur.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");
        }

    }

}
