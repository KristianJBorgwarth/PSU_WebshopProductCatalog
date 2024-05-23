using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }
    }
}
