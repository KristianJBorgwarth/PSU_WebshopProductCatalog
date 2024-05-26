using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.BookStore.Application.Features.Book.Commands.CreateBook;
using Webshop.BookStore.Application.Features.Book.Commands.DeleteBook;
using Webshop.BookStore.Application.Features.Book.Commands.UpdateBook;
using Webshop.BookStore.Application.Features.Book.Queries.GetBook;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooks;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooksByCategory;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooksBySeller;
using Webshop.BookStore.Application.Features.Book.Requests;
using Webshop.Customer.Api.Controllers;
using static System.String;

namespace Webshop.BookStore.Api.Controllers;

[Route("api/bookstore/book")]
public class BookController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<BookController> _logger;

    public BookController(IMediator mediator, ILogger<BookController> logger, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook(CreateBookRequest request)
    {
        CreateBookRequest.Validator validator = new();
        var result = await validator.ValidateAsync(request);

        if(result.IsValid)
        {
            var command = _mapper.Map<CreateBookCommand>(request);
            var createResult = await _mediator.Send(command);
            return createResult.Success ? Ok(createResult) : Error(createResult.Error);
        }
        else
        {
            _logger.LogError(Join(",", result.Errors.Select(x => x.ErrorMessage)));
            return Error(result.Errors);
        }
    }

    [HttpGet]
    [Route("category/{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBookByCategoryId(int categoryId)
    {
        var query = new GetBooksByCategoryQuery() {CategoryId = categoryId};
        var result = await _mediator.Send(query);

        if (!result.Success) return Error(result.Error);
        return result.Value.Any() ? Ok(result.Value) : NoContent();
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBooks()
    {
        var result = await _mediator.Send(new GetBooksQuery());

        if (!result.Success) return Error(result.Error);
        return result.Value.Any() ? Ok(result.Value) : NoContent();
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBookById(int id)
    {
        var result = await _mediator.Send(new GetBookQuery{BookId = id});

        if (!result.Success) return Error(result.Error);
        return result.Value != null ? Ok(result.Value) : NoContent();
    }

    [HttpGet]
    [Route("seller/{sellerId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBookBySellerId(int sellerId)
    {
        var query = new GetBooksBySellerQuery {SellerId = sellerId};
        var result = await _mediator.Send(query);

        if (!result.Success) return Error(result.Error);
        return result.Value.Any() ? Ok(result.Value) : NoContent();
    }

    [HttpPut]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBook([FromBody]UpdateBookRequest request)
    {
        UpdateBookRequest.Validator validator = new();
        var result = await validator.ValidateAsync(request);
        if(result.IsValid)
        {
            var command = _mapper.Map<UpdateBookCommand>(request);
            var updateResult = await _mediator.Send(command);
            return updateResult.Success ? Ok(updateResult) : Error(updateResult.Error);
        }
        else
        {
            _logger.LogError(Join(",", result.Errors.Select(x => x.ErrorMessage)));
            return Error(result.Errors);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var command = new DeleteBookCommand{BookId = id};
        var result = await _mediator.Send(command);
        return result.Success ? Ok(result) : Error(result.Error);
    }
}