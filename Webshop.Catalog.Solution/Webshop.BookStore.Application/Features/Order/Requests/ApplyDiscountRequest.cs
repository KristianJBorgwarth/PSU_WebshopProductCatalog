using FluentValidation;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Requests;

public class ApplyDiscountRequest
{
    public int OrderId { get; set; }
    public decimal Discount { get; set; }

    public class Validator : AbstractValidator<ApplyDiscountRequest>
    {
        public Validator()
        {
            RuleFor(x=> x.OrderId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(OrderId)).Code).GreaterThanOrEqualTo(0)
                .WithMessage(Errors.General.ValueTooSmall(nameof(OrderId), 0 ).Code);
            RuleFor(x => x.Discount).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Discount)).Code);
        }
    }
}