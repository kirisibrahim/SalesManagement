using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Validators
{
    public class InvoiceValidator : AbstractValidator<InvoiceDto>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.InvoiceNumber).NotEmpty().WithMessage("Fatura numarası boş olamaz.");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("Toplam tutar 0'dan büyük olmalıdır.");
        }
    }
}
