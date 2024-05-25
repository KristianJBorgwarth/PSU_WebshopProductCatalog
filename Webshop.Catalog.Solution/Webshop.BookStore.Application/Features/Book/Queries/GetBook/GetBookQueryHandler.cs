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
        throw new NotImplementedException();
    }
}