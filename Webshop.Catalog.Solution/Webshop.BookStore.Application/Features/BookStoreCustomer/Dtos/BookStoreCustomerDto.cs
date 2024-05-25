using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Dtos
{
    public class BookStoreCustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseCustomeerId { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }
    }
}
