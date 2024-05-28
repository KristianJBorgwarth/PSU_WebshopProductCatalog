using Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Services.PaymentService;

public interface IPaymentService
{
    Task<Result> ProcessPayment(PaymentRequest paymentRequest);
}