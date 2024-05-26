using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.BookStore.Application.Features.Book.Requests;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomers;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Requests;
using Webshop.Customer.Api.Controllers;
using Webshop.Domain.Common;
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
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        CreateCustomerRequest.Validator validator = new();
        var result = await validator.ValidateAsync(request);
        if (result.IsValid)
        {
            var command = _mapper.Map<CreateCustomerCommand>(request);
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
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest request)
    {
        UpdateCustomerCommand command = _mapper.Map<UpdateCustomerCommand>(request);
        Result result = await _mediator.Send(command);

        return result.Success ? Ok(result) : Error(result.Error);
    }

    [HttpDelete]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCustomer([FromBody] DeleteCustomerRequest request)
    {
        DeleteCustomerCommand command = _mapper.Map<DeleteCustomerCommand>(request);
        Result result = await _mediator.Send(command);

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
}