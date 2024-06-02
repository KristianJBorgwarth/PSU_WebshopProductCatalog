﻿using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.BookStore.Application.Features.Order.Commands.AddOrderItemCommand;
using Webshop.BookStore.Application.Features.Order.Commands.CreateOrder;
using Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;
using Webshop.BookStore.Application.Features.Order.Queries.GetOrders;
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

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOrders()
    {
        var result = await _mediator.Send(new GetOrdersQuery());

        if(result.Success is false) return Error(result.Error);
        return result.Value.Any() ? Ok(result.Value) : NoContent();
    }

    [HttpPut]
    [Route("orderItem")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItemToOrder(AddOrderItemRequest request)
    {
        AddOrderItemRequest.Validator validator = new();
        var result = await validator.ValidateAsync(request);

        if(result.IsValid)
        {
            var command = _mapper.Map<AddOrderItemCommand>(request);
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
    [Route("discount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ApplyDiscount(ApplyDiscountRequest request)
    {
        ApplyDiscountRequest.Validator validator = new();
        var result = await validator.ValidateAsync(request);

        if(result.IsValid)
        {
            var command = _mapper.Map<ApplyDiscountCommand>(request);
            var createResult = await _mediator.Send(command);
            return createResult.Success ? Ok(createResult) : Error(createResult.Error);
        }
        else
        {
            _logger.LogError(Join(",", result.Errors.Select(x => x.ErrorMessage)));
            return Error(result.Errors);
        }
    }

    [HttpPost]
    [Route("process")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ProcessOrder(ProcessOrderRequest request)
    {
        ProcessOrderRequest.Validator validator = new();
        var result = await validator.ValidateAsync(request);

        if(result.IsValid)
        {
            var command = _mapper.Map<ProcessOrderCommand>(request);
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