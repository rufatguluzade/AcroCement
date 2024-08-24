using Business.DTOs.Blog.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Blog
{
    public class BlogCreateDTOValidator :AbstractValidator<BlogCreateDTO>
    {
        public BlogCreateDTOValidator()
        {
            RuleFor(x => x.Title)
                  .NotEmpty()
                  .WithMessage("title daxil edilmelidir")
                  .NotNull().WithMessage("title null ola bilmez");



            RuleFor(x => x.Description)
              .NotEmpty()
              .WithMessage("Description daxil edilmelidir")
              .NotNull().WithMessage("description null ola bilmez");

            RuleFor(x => x.Author)
              .NotEmpty()
              .WithMessage("Author daxil edilmelidir")
              .MaximumLength(50)
              .WithMessage("max chracter sayi 50 olmalidir")
              .NotNull().WithMessage("Author null ola bilmez");



            RuleFor(x => x.ImageFile)
        .NotEmpty()
        .WithMessage("image daxil edilmelidir");



         
        }
    }
}
