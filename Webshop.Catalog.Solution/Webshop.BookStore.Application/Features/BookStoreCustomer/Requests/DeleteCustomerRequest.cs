using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.BookStore.Application.Features.Requests
{
    public class DeleteCustomerRequest
    {
        public required Guid CustomerId { get; set; }
    }
}
