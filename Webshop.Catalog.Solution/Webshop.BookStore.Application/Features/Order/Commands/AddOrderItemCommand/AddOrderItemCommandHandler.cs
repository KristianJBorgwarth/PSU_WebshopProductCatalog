using MediatR;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.AddOrderItemCommand;

public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<AddOrderItemCommandHandler> _logger;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository, ILogger<AddOrderItemCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetById(request.OrderId);
            if (order is null) return Result.Fail(Errors.General.NotFound(request.OrderId));

            order.AddItem(request.OrderItem);

            await _orderRepository.UpdateAsync(order);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Fail(Errors.General.UnspecifiedError(ex.Message));
        }
    }
}