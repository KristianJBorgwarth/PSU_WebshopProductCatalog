using FluentValidation;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Requests
{
    public class UpdateBookStoreCustomerRequest
    {
        public int Id { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }

        public class Validator : AbstractValidator<UpdateBookStoreCustomerRequest>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Id)).Code).GreaterThanOrEqualTo(0)
                    .WithMessage(Errors.General.ValueTooSmall(nameof(Id), 1).Code);

                RuleFor(x => x.IsSeller)
                    .NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(IsSeller)).Code);

                RuleFor(x => x.IsBuyer).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(IsBuyer)).Code);
            }
        }

    }
}
