using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Order.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<List<OrderDto>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<GetOrdersQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<GetOrdersQueryHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderRepository.GetAll();
            var mappedOrders = _mapper.Map<List<OrderDto>>(orders);
            return mappedOrders;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Fail<List<OrderDto>>(Errors.General.UnspecifiedError(ex.Message));
        }
    }
}