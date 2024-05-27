using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CreateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = _mapper.Map<Domain.AggregateRoots.Order>(request);
            await _orderRepository.CreateAsync(order);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, ex.Message);
            return Result.Fail(Errors.General.UnspecifiedError(ex.Message));
        }
    }
}