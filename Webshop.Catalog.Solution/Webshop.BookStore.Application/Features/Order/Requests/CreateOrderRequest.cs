using FluentValidation;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Requests;

public class CreateOrderRequest
{
    public int BuyerId { get; set; }

    public class Validator : AbstractValidator<CreateOrderRequest>
    {
        public Validator()
        {
            RuleFor(x => x.BuyerId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(BuyerId)).Code).GreaterThan(0)
                .WithMessage(Errors.General.ValueTooSmall(nameof(BuyerId), 1).Code);
        }
    }
}