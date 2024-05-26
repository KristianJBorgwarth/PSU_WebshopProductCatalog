using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
{
    private readonly IBookRepository _bookRepository;
    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookToDelete = await _bookRepository.GetById(request.BookId);

            if (bookToDelete == null)
            {
                return Result.Fail(Errors.General.NotFound(request.BookId));
            }

            await _bookRepository.DeleteAsync(bookToDelete.Id);

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(Errors.General.UnspecifiedError(e.Message));
        }
    }
}