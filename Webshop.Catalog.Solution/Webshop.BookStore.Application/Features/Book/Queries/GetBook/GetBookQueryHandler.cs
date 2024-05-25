using AutoMapper;
using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Queries.GetBook;

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Result<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<Result<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookRepository.GetById(request.BookId);

            if (book == null) return Result.Fail<BookDto>(Errors.General.NotFound(request.BookId));

            var bookDto = _mapper.Map<BookDto>(book);
            return Result.Ok(bookDto);
        }
        catch (Exception e)
        {
            return Result.Fail<BookDto>(Errors.General.UnspecifiedError(e.Message));
        }
    }
}