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
