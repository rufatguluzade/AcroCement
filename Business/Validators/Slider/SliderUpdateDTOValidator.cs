using Business.DTOs.Slider.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Slider
{
    public class SliderUpdateDTOValidator :AbstractValidator<SliderUpdateDTO>
    {
        public SliderUpdateDTOValidator()
        {
            RuleFor(x => x.Title)
    .NotEmpty().WithMessage("Title bos ola bilmez")
    .NotNull().WithMessage("Title null ola bilmez");

            RuleFor(x => x.Description)
        .NotEmpty().WithMessage("Description bos ola bilmez")
        .NotNull().WithMessage("Description null ola bilmez");


            RuleFor(x => x.ImageFile)
              .NotEmpty()
              .WithMessage("image daxil edilmelidir");
        }
    }
}
