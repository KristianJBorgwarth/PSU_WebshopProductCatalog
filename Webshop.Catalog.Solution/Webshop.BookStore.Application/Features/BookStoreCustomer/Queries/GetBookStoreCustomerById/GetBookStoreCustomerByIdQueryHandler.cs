using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomerById
{
    public class GetBookStoreCustomerByIdQueryHandler : IRequestHandler<GetBookStoreCustomerByIdQuery, Result<BookStoreCustomerDto>>
    {
        private readonly IBookStoreCustomerRepository _bookStoreCustomerRepository;
        private readonly IMapper _mapper;

        public GetBookStoreCustomerByIdQueryHandler(IBookStoreCustomerRepository bookStoreCustomerRepository, IMapper mapper)
        {
            _bookStoreCustomerRepository = bookStoreCustomerRepository;
            _mapper = mapper;
        }       

        public async Task<Result<BookStoreCustomerDto>> Handle(GetBookStoreCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _bookStoreCustomerRepository.GetById(request.id);
                var result = _mapper.Map<BookStoreCustomerDto>(customer);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<BookStoreCustomerDto>(Errors.General.UnspecifiedError(ex.Message));
            }
        }
    }
}
