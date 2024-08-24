using Business.DTOs.AboutUs.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.AboutUs
{
    public class AboutUsUpdateDTOValidator :AbstractValidator<AboutUsUpdateDTO>
    {
        public AboutUsUpdateDTOValidator()
        {
            RuleFor(x => x.Description)
            .NotEmpty()
         .WithMessage("Description daxil edilmelidir");


            RuleFor(x => x.ImageFile)
              .NotEmpty()
              .WithMessage("image daxil edilmelidir");
        }
    }
}
