using FluentValidation;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Requests;

public class ProcessOrderRequest
{
    public int OrderId { get; set; }
    public int Amount { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public int CVC { get; set; }

    public class Validator : AbstractValidator<ProcessOrderRequest>
    {
        public Validator()
        {
            RuleFor(x=> x.OrderId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(OrderId)).Code).GreaterThanOrEqualTo(0)
                .WithMessage(Errors.General.ValueTooSmall(nameof(OrderId), 1).Code);
            RuleFor(x => x.Amount).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Amount)).Code).GreaterThanOrEqualTo(0).WithMessage(Errors.General.ValueTooSmall(nameof(Amount), 1).Code);
            RuleFor(x => x.CardNumber).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(CardNumber)).Code).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(CardNumber)).Code);
            RuleFor(x => x.ExpirationDate).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(ExpirationDate)).Code).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(ExpirationDate)).Code);
            RuleFor(x => x.CVC).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(CVC)).Code).GreaterThanOrEqualTo(100).WithMessage(Errors.General.ValueTooSmall(nameof(CVC), 100).Code).LessThan(1000).WithMessage(Errors.General.ValueTooLarge(nameof(CVC), 999).Code);
        }
    }
}