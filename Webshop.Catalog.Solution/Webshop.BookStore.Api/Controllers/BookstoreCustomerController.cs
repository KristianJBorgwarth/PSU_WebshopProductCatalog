using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomerById;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomers;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Requests;
using Webshop.Customer.Api.Controllers;
using static System.String;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateBookStoreCustomerRequest request)
    {
        CreateBookStoreCustomerRequest.Validator validator = new();
        var result = await validator.ValidateAsync(request);
        if (result.IsValid)
        {
            var command = _mapper.Map<CreateBookStoreCustomerCommand>(request);
            var createResult = await _mediator.Send(command);
            return createResult.Success ? Ok(createResult) : Error(createResult.Error);
        }
        else
        {
            _logger.LogError(Join(",", result.Errors.Select(x => x.ErrorMessage)));
            return Error(result.Errors);
        }
    }

    [HttpPut]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateBookStoreCustomerRequest request)
    {
        UpdateBookStoreCustomerRequest.Validator validator = new();
        var result = await validator.ValidateAsync(request);
        if (result.IsValid)
        {
            UpdateBookStoreCustomerCommand command = _mapper.Map<UpdateBookStoreCustomerCommand>(request);
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
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var result = await _mediator.Send(new DeleteBookStoreCustomerCommand { CustomerId = id });

        return result.Success ? Ok(result) : Error(result.Error);
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBookStoreCustomers()
    {
        var result = await _mediator.Send(new GetBookStoreCustomersQuery());

        if (!result.Success) return Error(result.Error);
        return result.Value.Any() ? Ok(result.Value) : NoContent();
    }
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBookStoreCustomerById(int id)
    {
        GetBookStoreCustomerByIdQuery query = new GetBookStoreCustomerByIdQuery() { id = id}; 
        var result = await _mediator.Send(query);

        if (!result.Success) return Error(result.Error);
        return result.Value != null ? Ok(result.Value) : NoContent();
    }
}
