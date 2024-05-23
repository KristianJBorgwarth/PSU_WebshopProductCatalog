using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
    {
        private readonly IBookStoreCustomerRepository _bookStoreCustomerRepository;

        public UpdateCustomerCommandHandler(IBookStoreCustomerRepository bookStoreCustomerRepository)
        {
            _bookStoreCustomerRepository = bookStoreCustomerRepository;
        }

        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Result result = await _bookStoreCustomerRepository.UpdateCustomer(request.Id, request.IsSeller, request.IsBuyer);
            if (!result.Success) return Result.Fail(result.Error);
            return Result.Ok();
        }
    }
}
