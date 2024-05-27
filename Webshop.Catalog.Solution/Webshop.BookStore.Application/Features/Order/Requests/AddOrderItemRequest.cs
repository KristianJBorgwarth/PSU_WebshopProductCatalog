using FluentValidation;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Requests;

public class AddOrderItemRequest
{
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public class Validator : AbstractValidator<AddOrderItemRequest>
    {
        public Validator()
        {
            RuleFor(r => r.OrderId)
                .NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(OrderId)).Code)
                .GreaterThanOrEqualTo(0).WithMessage(Errors.General.ValueTooSmall(nameof(OrderId), 1).Code);
            RuleFor(r => r.BookId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(BookId)).Code)
                .GreaterThan(0).WithMessage(Errors.General.ValueTooSmall(nameof(BookId), 1).Code);
            RuleFor(r => r.BookTitle).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(BookTitle)).Code);
            RuleFor(r => r.Price).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Price)).Code)
                .GreaterThan(0).WithMessage(Errors.General.ValueTooSmall(nameof(Price), 1).Code);
            RuleFor(r => r.Quantity).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Quantity)).Code);
        }
    }
}