using MediatR;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<Result>
{
    public int CustomerId { get; set; }
    public bool IsSeller { get; set; }
    public bool IsBuyer { get; set; }
}