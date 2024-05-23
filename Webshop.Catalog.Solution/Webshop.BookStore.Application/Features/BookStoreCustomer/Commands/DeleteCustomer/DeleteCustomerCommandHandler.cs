using MediatR;
using System.Reflection.Metadata.Ecma335;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
    {
        private readonly IBookStoreCustomerRepository _bookStoreCustomerRepository;

        public DeleteCustomerCommandHandler(IBookStoreCustomerRepository bookStoreCustomerRepository)
        {
            _bookStoreCustomerRepository = bookStoreCustomerRepository;
        }

        public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            Result result = await _bookStoreCustomerRepository.DeleteCustomer(request.CustomerId);
            return result.Success ? Result.Ok() : Result.Fail(result.Error);
        }
    }
}
