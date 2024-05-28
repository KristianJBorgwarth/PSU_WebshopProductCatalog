using MediatR;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Result>
{
    public int BuyerId { get; set; }
}