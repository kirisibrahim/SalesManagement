using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Müşteri adı boş olamaz.")
                .MaximumLength(200).WithMessage("Müşteri adı en fazla 200 karakter olabilir.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(75).WithMessage("E-posta adresi en fazla 75 karakter olabilir.");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                //.Matches(@"^\+?[1-9][0-9]{7,14}$").WithMessage("Geçerli bir telefon numarası giriniz.") uluslararası format
                .MaximumLength(15).WithMessage("Telefon numarası en fazla 15 karakter olabilir.");

            RuleFor(c => c.Address)
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");
        }
    }
}
