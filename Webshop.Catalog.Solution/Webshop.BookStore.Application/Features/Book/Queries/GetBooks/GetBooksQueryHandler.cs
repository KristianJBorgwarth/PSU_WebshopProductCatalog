using AutoMapper;
using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Queries.GetBooks;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, Result<List<BookDto>>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    public GetBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<BookDto>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var books =  await _bookRepository.GetAll();
            var result = _mapper.Map<List<BookDto>>(books);
            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<BookDto>>(Errors.General.UnspecifiedError(ex.Message));
        }
    }
}