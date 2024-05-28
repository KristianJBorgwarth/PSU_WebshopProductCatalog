using MediatR;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer
{
    public class DeleteBookStoreCustomerCommand : IRequest<Result>
    {
        public int CustomerId { get; set; }
    }
}
