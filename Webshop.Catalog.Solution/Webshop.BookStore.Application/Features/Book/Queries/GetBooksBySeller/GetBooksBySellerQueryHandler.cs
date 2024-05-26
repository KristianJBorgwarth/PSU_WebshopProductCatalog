using AutoMapper;
using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Queries.GetBooksBySeller;

public class GetBooksBySellerQueryHandler : IRequestHandler<GetBooksBySellerQuery, Result<List<BookDto>>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBooksBySellerQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<BookDto>>> Handle(GetBooksBySellerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var books = await _bookRepository.GetBooksBySeller(request.SellerId);
            var result = _mapper.Map<List<BookDto>>(books);
            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Fail<List<BookDto>>(Errors.General.UnspecifiedError(e.Message));
        }
    }
}