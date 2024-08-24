using Business.DTOs.CustomerReaction.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.CustomerReaction
{
    public class CustomerReactionUpdateDTOValidator :AbstractValidator<CustomerReactionUpdateDTO>
    {
        public CustomerReactionUpdateDTOValidator()
        {
            RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName bos ola bilmez")
            .NotNull().WithMessage(" FullName null ola bilmez");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position bos ola bilmez")
                .NotNull().WithMessage("Position null ola bilmez");

            RuleFor(x => x.Reaction)
                     .NotEmpty().WithMessage("Reaction bos ola bilmez")
                     .NotNull().WithMessage("Reaction null ola bilmez");

        }
    }
}
