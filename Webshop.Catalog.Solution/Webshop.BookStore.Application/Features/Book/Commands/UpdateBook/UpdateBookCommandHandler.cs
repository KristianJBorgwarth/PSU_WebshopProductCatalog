using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateBookCommandHandler> _logger;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper, ILogger<UpdateBookCommandHandler> logger)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookToUpdate = await _bookRepository.GetById(request.Id);

            if (bookToUpdate == null)
            {
                _logger.LogError($"Book with id {request.Id} not found.");
                return Result.Fail(Errors.General.NotFound(request.Id));
            }

            _mapper.Map(request, bookToUpdate);

            await _bookRepository.UpdateAsync(bookToUpdate);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, ex.Message);
            return Result.Fail(Errors.General.UnspecifiedError(ex.Message));
        }
    }
}