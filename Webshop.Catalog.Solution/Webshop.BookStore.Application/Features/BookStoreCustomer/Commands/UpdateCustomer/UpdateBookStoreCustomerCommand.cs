using MediatR;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer
{
    public class UpdateBookStoreCustomerCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }
    }
}
