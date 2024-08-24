using Business.DTOs.Contact.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Contact
{
    public class ContactUpdateDTOValidator :AbstractValidator<ContactUpdateDTO>
    {
        public ContactUpdateDTOValidator()
        {
            RuleFor(x => x.MapUrl)
     .NotEmpty().WithMessage("MapUrl bos ola bilmez")
     .NotNull().WithMessage(" MapUrl null ola bilmez");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone bos ola bilmez")
                .NotNull().WithMessage("Phone null ola bilmez");

            RuleFor(x => x.Email)
                     .NotEmpty().WithMessage("Email bos ola bilmez")
                     .NotNull().WithMessage("Email null ola bilmez")
                     .EmailAddress().WithMessage("Email adresu duzgun sekilde yazin");

            RuleFor(x => x.Adress)
         .NotEmpty().WithMessage("Adress bos ola bilmez")
         .NotNull().WithMessage("Adress null ola bilmez");

            RuleFor(x => x.BusinessHours)
        .NotEmpty().WithMessage("BusinessHours bos ola bilmez")
        .NotNull().WithMessage("BusinessHours null ola bilmez");


            RuleFor(x => x.ImageFile)
              .NotEmpty()
              .WithMessage("image daxil edilmelidir");
        }
    }
}
