using MediatR;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Services.PaymentService;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;

public class ProcessOrderCommandHandler : IRequestHandler<ProcessOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<ProcessOrderCommandHandler> _logger;
    private readonly IPaymentService _paymentService;

    public ProcessOrderCommandHandler(IPaymentService paymentService, IOrderRepository orderRepository, ILogger<ProcessOrderCommandHandler> logger)
    {
        _paymentService = paymentService;
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetById(request.OrderId);
            if (order == null)
            {
                _logger.LogError($"Order with id {request.OrderId} was not found.");
                return Result.Fail(Errors.General.NotFound(request.OrderId));
            }

            if (!order.CanProcessPayment())
            {
                return Result.Fail(Errors.General.UnspecifiedError("Order cannot be processed."));
            }

            var paymentResult = await _paymentService.ProcessPayment(request.PaymentDetails);

            if (paymentResult.Success)
            {
                order.Status = OrderStatus.Completed;
                await _orderRepository.UpdateAsync(order);
                return Result.Ok();
            }
            else
            {
                order.Status = OrderStatus.Failed;
                await _orderRepository.UpdateAsync(order);
                return Result.Fail(Errors.General.UnspecifiedError("An error occurred while processing the payment."));
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return Result.Fail(Errors.General.UnspecifiedError("An error occurred while processing the order."));
        }
    }
}