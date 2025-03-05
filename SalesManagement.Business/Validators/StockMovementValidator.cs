using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Validators
{
    public class StockMovementValidator : AbstractValidator<StockMovementDto>
    {
        public StockMovementValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId 0'dan büyük olmalıdır.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");
            RuleFor(x => x.MovementDate).NotEmpty().WithMessage("MovementDate boş olamaz.");
            RuleFor(x => x.MovementType).NotEmpty().WithMessage("MovementType boş olamaz..");
            RuleFor(X => X.MovementType).IsInEnum().WithMessage("Geçersiz MovementType. 'IN' veya 'OUT' olmalıdır");
        }
    }
}
