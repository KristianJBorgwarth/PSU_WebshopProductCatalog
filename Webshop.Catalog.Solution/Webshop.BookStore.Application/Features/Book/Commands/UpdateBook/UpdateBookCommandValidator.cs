using FluentValidation;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Commands.UpdateBook;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(UpdateBookCommand.Id)).Code)
            .GreaterThanOrEqualTo(0).WithMessage(Errors.General.ValueTooSmall(nameof(UpdateBookCommand.Id), 1).Code);
        RuleFor(r => r.Title).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(UpdateBookCommand.Title)).Code);
        RuleFor(r => r.Author).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(UpdateBookCommand.Author)).Code);
        RuleFor(r => r.Description).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(UpdateBookCommand.Description)).Code);
        RuleFor(r => r.Price).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(UpdateBookCommand.Price)).Code);
        RuleFor(r => r.CategoryId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(UpdateBookCommand.CategoryId)).Code);
        RuleFor(r => r.SellerId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(UpdateBookCommand.SellerId)).Code);
    }
}