using MediatR;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;

public class ApplyDiscountCommandHandler : IRequestHandler<ApplyDiscountCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<ApplyDiscountCommandHandler> _logger;

    public ApplyDiscountCommandHandler(IOrderRepository orderRepository, ILogger<ApplyDiscountCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetById(request.OrderId);
            if (order == null) return Result.Fail(Errors.General.NotFound(request.OrderId));
            order.ApplyDiscount(request.Discount);
            await _orderRepository.UpdateAsync(order);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, ex.Message);
            return Result.Fail(Errors.General.UnspecifiedError(ex.Message));
        }
    }
}