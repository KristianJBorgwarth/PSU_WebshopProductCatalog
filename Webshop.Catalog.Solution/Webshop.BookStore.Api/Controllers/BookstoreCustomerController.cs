using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.BookStore.Application.Features.Requests;
using Webshop.Customer.Api.Controllers;

namespace Webshop.BookStore.Api.Controllers;

[Route("api/bookstore/customer")]
public class BookstoreCustomerController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookstoreCustomerController> _logger;
    private readonly IMapper _mapper;

    public BookstoreCustomerController(IMediator mediator, ILogger<BookstoreCustomerController> logger, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        return Ok();
    }
}