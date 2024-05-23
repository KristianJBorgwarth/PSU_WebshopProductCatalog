using AutoMapper;
using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Queries.GetBooksByCategory;

public class GetBooksByCategoryQueryHandler : IRequestHandler<GetBooksByCategoryQuery, Result<List<BookDto>>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBooksByCategoryQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<BookDto>>> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var books = await _bookRepository.GetBooksByCategory(request.CategoryId);
            var result = new List<BookDto>();
            //TODO: THIS SHOULD BE MAPPED!
            return result;
        }
        catch(Exception ex)
        {
            return Result.Fail<List<BookDto>>(Errors.General.UnspecifiedError(ex.Message));
        }
    }
}