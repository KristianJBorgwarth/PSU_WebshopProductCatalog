using MediatR;
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
            if (!result.Success) return Result.Fail(result.Error);
            return Result.Ok();
        }
    }
}
