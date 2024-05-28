using MediatR;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;

public class ProcessOrderCommand : IRequest<Result>
{
    public int OrderId { get; set; }
    public PaymentRequest PaymentDetails { get; set; }
}

public class PaymentRequest
{
    public int Amount { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public int CVC { get; set; }
}