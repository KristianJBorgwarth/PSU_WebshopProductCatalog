using MediatR;
using Webshop.BookStore.Application.Features.Order.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Queries.GetOrders;

public class GetOrdersQuery : IRequest<Result<List<OrderDto>>>
{

}