using MediatR;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;

public class ApplyDiscountCommand : IRequest<Result>
{
    public int OrderId { get; set; }
    public decimal Discount { get; set; }
}