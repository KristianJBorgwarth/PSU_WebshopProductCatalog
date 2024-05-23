using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;
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
    [Route("update")]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest request)
    {
        Console.WriteLine("Hej");
        UpdateCustomerCommand command = _mapper.Map<UpdateCustomerCommand>(request); 
        Result result = await _mediator.Send(command);

        return result.Success ? Ok() : BadRequest(result.Error);
    }
    /*
     * {
  "id": "4CCC108E-9DCA-4C82-9A54-08DC7B0E6DBF6",
  "isSeller": false,
  "isBuyer": true
}
    */
}
