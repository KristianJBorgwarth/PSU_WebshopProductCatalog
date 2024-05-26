using FluentValidation;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Requests;

public class CreateBookStoreCustomerRequest
{
    public required int CustomerId { get; set; }
    public bool IsSeller { get; set; }
    public bool IsBuyer { get; set; }

    public class Validator : AbstractValidator<CreateBookStoreCustomerRequest>
    {
        public Validator()
        {
            RuleFor(x => x.CustomerId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(CustomerId)).Code).GreaterThan(0)
                .WithMessage(Errors.General.ValueTooSmall(nameof(CustomerId), 1).Code);

            RuleFor(x => x.IsSeller)
                .NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(IsSeller)).Code);

            RuleFor(x => x.IsBuyer).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(IsBuyer)).Code);
        }
    }

}