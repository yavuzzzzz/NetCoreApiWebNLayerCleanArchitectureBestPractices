using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace App.Services.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün Adı gereklidir!")
                .Length(3,20).WithMessage("Ürün ismi 3 ile 20 karakter arasında olmalıdır! ");


            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır!");

            RuleFor(x => x.Stock).InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasında olmalıdır!");

        }

    }
}
