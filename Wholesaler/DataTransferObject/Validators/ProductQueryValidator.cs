using FluentValidation;
using Wholesaler.Models;

namespace Wholesaler.DataTransferObject.Validators
{
    public class ProductQueryValidator : AbstractValidator<ProductQuery>
    {
        private int[] allowedPageSize = new[] { 3, 5, 10 };
        public ProductQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSize.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSize)}]");
                }
            });
        }
    }
}
