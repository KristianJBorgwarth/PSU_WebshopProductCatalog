using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomers;
using Webshop.BookStore.Application.Features.Requests;
using Webshop.Customer.Api.Controllers;
using Webshop.Domain.Common;

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
    [Route("")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        CreateCustomerCommand command = _mapper.Map<CreateCustomerCommand>(request);
        Result result = await _mediator.Send(command);

        return result.Success ? Ok() : BadRequest(result.Error);
    }
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest request)
    {
        UpdateCustomerCommand command = _mapper.Map<UpdateCustomerCommand>(request);
        Result result = await _mediator.Send(command);

        return result.Success ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteCustomer([FromBody] DeleteCustomerRequest request)
    {
        DeleteCustomerCommand command = _mapper.Map<DeleteCustomerCommand>(request);
        Result result = await _mediator.Send(command);

        return result.Success ? Ok() : BadRequest(result.Error);
    }
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBookStoreCustomers()
    {
        var result = await _mediator.Send(new GetBookStoreCustomersQuery());
        
        if (!result.Success) return BadRequest(result.Error);
        return result.Value.Any() ? Ok(result.Value) : NoContent();
    }
}
