using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Services.CategoryService;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result>
{
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryService _categoryService;

    public CreateBookCommandHandler(IBookRepository bookRepository, ICategoryService categoryService)
    {
        _bookRepository = bookRepository;
        _categoryService = categoryService;
    }

    public async Task<Result> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        Result<CategoryResult> result =  await _categoryService.GetCategoryAsync(request.CategoryId);

        if (!result.Success) return Result.Fail(result.Error);

        try
        {
            await _bookRepository.AddBook(request.Title, request.Author, request.Description, request.Price, request.CategoryId, request.SellerId);
        }
        catch (Exception e)
        {
            return Result.Fail(Errors.General.UnspecifiedError(e.Message));
        }

        return Result.Ok();
    }
}