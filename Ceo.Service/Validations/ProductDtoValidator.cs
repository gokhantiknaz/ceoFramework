using Ceo.Core.Dtos;
using FluentValidation;

namespace Ceo.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductDtoValidator()
        {
            //--https://docs.fluentvalidation.net/en/latest/built-in-validators.html
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} alanı boş geçilemez!").NotEmpty().WithMessage("{PropertyName} alanı boş geçilemez!");

            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan büyük olmalıdır.");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan büyük olmalıdır.");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan büyük olmalıdır.");
        }
    }
}
