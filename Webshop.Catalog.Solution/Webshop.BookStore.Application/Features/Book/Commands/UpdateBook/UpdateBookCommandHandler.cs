using AutoMapper;
using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}