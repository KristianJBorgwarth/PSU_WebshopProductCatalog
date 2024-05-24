using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Services.CustomerService;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result>
{
    private readonly ICustomerService _customerService;
    private readonly IBookStoreCustomerRepository _bookStoreCustomerRepository;

    public CreateCustomerCommandHandler(ICustomerService customerService, IBookStoreCustomerRepository bookStoreCustomerRepository)
    {
        _customerService = customerService;
        _bookStoreCustomerRepository = bookStoreCustomerRepository;
    }

    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
      Result<CustomerResult> result = await _customerService.GetCustomerAsync(request.CustomerId);

      if (!result.Success) return Result.Fail(result.Error);

      try
      {
        var customer = new Domain.AggregateRoots.BookstoreCustomer
        {
          Name = result.Value.Name,
          BaseCustomeerId = request.CustomerId,
          IsSeller = request.IsSeller,
          IsBuyer = request.IsBuyer
        };

        await _bookStoreCustomerRepository.CreateAsync(customer);
      }
      catch (Exception e)
      {
        return Result.Fail(Errors.General.UnspecifiedError(e.Message));
      }
      return Result.Ok();
    }
}