using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Result>
    {
        public Guid CustomerId { get; set; }
    }
}
