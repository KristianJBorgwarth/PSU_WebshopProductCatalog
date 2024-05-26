using FluentValidation;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Requests;

public class CreateBookRequest
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }

    public class Validator : AbstractValidator<CreateBookRequest>
    {
        public Validator()
        {
            RuleFor(r => r.Title).NotEmpty()
                .WithMessage(Errors.General.ValueIsRequired(nameof(Title)).Code);
            RuleFor(r=> r.Author).NotEmpty()
                .WithMessage(Errors.General.ValueIsRequired(nameof(Author)).Code);
            RuleFor(r=> r.Description).NotEmpty()
                .WithMessage(Errors.General.ValueIsRequired(nameof(Description)).Code);
            RuleFor(r => r.Price).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Price)).Code).GreaterThan(0)
                .WithMessage(Errors.General.ValueTooSmall(nameof(Price), 0).Code);
            RuleFor(r => r.CategoryId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(CategoryId)).Code);
            RuleFor(r => r.SellerId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(SellerId)).Code).GreaterThan(0)
                .WithMessage(Errors.General.ValueTooSmall(nameof(SellerId), 1).Code);
        }
    }
}