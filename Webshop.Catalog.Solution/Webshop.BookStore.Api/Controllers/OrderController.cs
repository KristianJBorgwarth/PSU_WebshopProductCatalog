using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.BookStore.Application.Features.Order.Commands.CreateOrder;
using Webshop.BookStore.Application.Features.Order.Requests;
using Webshop.Customer.Api.Controllers;
using static System.String;

namespace Webshop.BookStore.Api.Controllers;

[Route("api/bookstore/order")]
public class OrderController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderController> _logger;
    private readonly IMapper _mapper;
    public OrderController(IMediator mediator, ILogger<OrderController> logger, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        CreateOrderRequest.Validator validator = new();
        var result = await validator.ValidateAsync(request);

        if(result.IsValid)
        {
            var command = _mapper.Map<CreateOrderCommand>(request);
            var createResult = await _mediator.Send(command);
            return createResult.Success ? Ok(createResult) : Error(createResult.Error);
        }
        else
        {
            _logger.LogError(Join(",", result.Errors.Select(x => x.ErrorMessage)));
            return Error(result.Errors);
        }
    }
}