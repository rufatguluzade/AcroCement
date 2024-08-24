using Business.DTOs.Product.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Product
{
    public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateDTOValidator()
        {
            RuleFor(x => x.Title)
                 .NotEmpty()
                 .WithMessage("title daxil edilmelidir")
                 .NotNull().WithMessage("title null ola bilmez");



            RuleFor(x => x.ShortDescription)
              .NotEmpty()
              .WithMessage("Description daxil edilmelidir")
              .NotNull().WithMessage("description null ola bilmez");

            RuleFor(x => x.ComplianceStandard)
              .NotEmpty()
              .WithMessage("ComplianceStandard daxil edilmelidir")
              .NotNull().WithMessage("ComplianceStandard null ola bilmez");


            RuleFor(x => x.AreasOfApplication)
              .NotEmpty()
              .WithMessage("AreasOfApplication daxil edilmelidir")
              .NotNull().WithMessage("AreasOfApplication null ola bilmez");

            RuleFor(x => x.Advantages)
             .NotEmpty()
             .WithMessage("Advantages daxil edilmelidir")
             .NotNull().WithMessage("Advantages null ola bilmez");

            RuleFor(x => x.Weight)
       .NotEmpty()
       .WithMessage("Weight daxil edilmelidir")
       .NotNull().WithMessage("Weight null ola bilmez");


            RuleFor(x => x.Manufacture)
       .NotEmpty()
       .WithMessage("Manufacture daxil edilmelidir")
       .NotNull().WithMessage("Manufacture null ola bilmez");

            RuleFor(x => x.ProductImageFiles)
.NotEmpty()
.WithMessage("ProductImageFiles daxil edilmelidir")
.NotNull().WithMessage("ProductImageFiles null ola bilmez");


        }
    }
}
