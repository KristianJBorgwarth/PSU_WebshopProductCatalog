using FluentValidation;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Requests;

public class UpdateBookRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    public class Validator : AbstractValidator<UpdateBookRequest>
    {
        public Validator()
        {
            RuleFor(r => r.Id)
                .NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Id)).Code)
                .GreaterThan(0).WithMessage(Errors.General.ValueTooSmall(nameof(Id), 1).Code);
            RuleFor(r => r.Title).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Title)).Code);
            RuleFor(r => r.Author).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Author)).Code);
            RuleFor(r => r.Description).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Description)).Code);
            RuleFor(r => r.Price).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Price)).Code).GreaterThan(0)
                .WithMessage(Errors.General.ValueTooSmall(nameof(Price), 0).Code);
            RuleFor(r => r.CategoryId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(CategoryId)).Code);
        }
    }

}