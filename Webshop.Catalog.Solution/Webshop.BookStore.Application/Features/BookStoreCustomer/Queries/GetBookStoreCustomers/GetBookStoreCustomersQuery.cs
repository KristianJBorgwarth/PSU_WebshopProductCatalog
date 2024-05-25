using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomers
{
    public class GetBookStoreCustomersQuery : IRequest<Result<List<BookStoreCustomerDto>>>
    {
    }
}
