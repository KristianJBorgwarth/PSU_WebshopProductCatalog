using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.BookStore.Application.Features.Book.Commands.CreateBook;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooksByCategory;
using Webshop.BookStore.Application.Features.Book.Requests;
using Webshop.Customer.Api.Controllers;

namespace Webshop.BookStore.Api.Controllers;

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
    [Route("{categoryid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]//Should we even do this?
    public async Task<ActionResult> GetBookByCategoryId(int id)
    {
        var query = new GetBooksByCategoryQuery() {CategoryId = id};
        var result = await _mediator.Send(query);

        if (!result.Success) return BadRequest(result.Error);
        return result.Value.Any() ? Ok(result.Value) : NoContent();
    }
}