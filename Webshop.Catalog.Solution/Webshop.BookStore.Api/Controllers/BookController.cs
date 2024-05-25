using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Webshop.BookStore.Application.Features.Book.Commands.CreateBook;
using Webshop.BookStore.Application.Features.Book.Queries.GetBook;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooks;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooksByCategory;
using Webshop.BookStore.Application.Features.Book.Requests;
using Webshop.Customer.Api.Controllers;

namespace Webshop.BookStore.Api.Controllers;

[Route("api/bookstore/book")]
public class BookController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookController> _logger;
    private readonly IMapper _mapper;

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
    public async Task<ActionResult> CreateBook(CreateBookRequest request)
    {
        var command = _mapper.Map<CreateBookCommand>(request);
        var result = await _mediator.Send(command);
        return result.Success ? Ok() : BadRequest(result.Error);
    }

    [HttpGet]
    [Route("category/{categoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetBookByCategoryId(int categoryId)
    {
        var query = new GetBooksByCategoryQuery() {CategoryId = categoryId};
        var result = await _mediator.Send(query);

        if (!result.Success) return BadRequest(result.Error);
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

        if (!result.Success) return BadRequest(result.Error);
        return result.Value.Any() ? Ok(result.Value) : NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBookById(int id)
    {
        var result = await _mediator.Send(new GetBookQuery(){BookId = id});

        if (!result.Success) return BadRequest(result.Error);
        return result.Value != null ? Ok(result.Value) : NoContent();
    }

}