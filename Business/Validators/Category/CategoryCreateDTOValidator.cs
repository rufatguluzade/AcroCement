using Business.DTOs.Category.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Category
{
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ad daxil edilmelidir")
                .MaximumLength(100)
                .WithMessage("max chracter sayi 100 olmalidir");

      



        }

    
    }
}
