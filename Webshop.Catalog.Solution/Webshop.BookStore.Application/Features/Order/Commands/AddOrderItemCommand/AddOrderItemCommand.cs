using MediatR;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.AddOrderItemCommand;

public class AddOrderItemCommand : IRequest<Result>
{
    public int OrderId { get; set; }
    public OrderItem OrderItem { get; set; }
}