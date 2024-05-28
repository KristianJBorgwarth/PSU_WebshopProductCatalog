using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer
{
    public class DeleteBookStoreCustomerCommandHandler : IRequestHandler<DeleteBookStoreCustomerCommand, Result>
    {
        private readonly IBookStoreCustomerRepository _bookStoreCustomerRepository;

        public DeleteBookStoreCustomerCommandHandler(IBookStoreCustomerRepository bookStoreCustomerRepository)
        {
            _bookStoreCustomerRepository = bookStoreCustomerRepository;
        }

        public async Task<Result> Handle(DeleteBookStoreCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _bookStoreCustomerRepository.GetById(request.CustomerId);
                if (customer == null) return Result.Fail(Errors.General.NotFound(request.CustomerId));

                await _bookStoreCustomerRepository.DeleteAsync(customer.Id);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(Errors.General.UnspecifiedError(e.Message));

            }
        }
    }
}
