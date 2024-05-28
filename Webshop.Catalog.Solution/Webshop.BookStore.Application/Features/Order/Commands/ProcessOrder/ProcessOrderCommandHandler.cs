using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Services.PaymentService;
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

            var paymentResult = await _paymentService.ProcessPayment(request.PaymentDetails);

            if (paymentResult.Success)
            {

                return Result.Ok();
            }
            else return Result.Fail(Errors.General.UnspecifiedError("An error occurred while processing the payment."));
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return Result.Fail(Errors.General.UnspecifiedError("An error occurred while processing the order."));
        }
    }
}