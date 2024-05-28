using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;

public class ProcessOrderCommandHandler : IRequestHandler<ProcessOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    public ProcessOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}