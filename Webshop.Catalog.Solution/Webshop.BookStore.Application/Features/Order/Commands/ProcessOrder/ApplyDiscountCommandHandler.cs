using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;

public class ApplyDiscountCommandHandler : IRequestHandler<ApplyDiscountCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    public ApplyDiscountCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}