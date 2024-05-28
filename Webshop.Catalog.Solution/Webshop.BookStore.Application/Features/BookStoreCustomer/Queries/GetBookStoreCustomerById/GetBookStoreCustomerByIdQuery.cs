using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomerById
{
    public class GetBookStoreCustomerByIdQuery : IRequest<Result<BookStoreCustomerDto>>
    {
        public int id { get; set; }
    }
}
