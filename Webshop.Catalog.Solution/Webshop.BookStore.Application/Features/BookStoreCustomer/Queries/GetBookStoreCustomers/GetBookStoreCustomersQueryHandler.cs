using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomers
{

    public class GetBookStoreCustomersQueryHandler : IRequestHandler<GetBookStoreCustomersQuery, Result<List<BookStoreCustomerDto>>>
    {
        private readonly IBookStoreCustomerRepository _bookStoreCustomerRepository;
        private readonly IMapper _mapper;

        public GetBookStoreCustomersQueryHandler(IBookStoreCustomerRepository bookStoreCustomerRepository, IMapper mapper)
        {
            _bookStoreCustomerRepository = bookStoreCustomerRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<BookStoreCustomerDto>>> Handle(GetBookStoreCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _bookStoreCustomerRepository.GetAll();
                var result = _mapper.Map<List<BookStoreCustomerDto>>(customers);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<BookStoreCustomerDto>>(Errors.General.UnspecifiedError(ex.Message));
            }
        }
    }
}
