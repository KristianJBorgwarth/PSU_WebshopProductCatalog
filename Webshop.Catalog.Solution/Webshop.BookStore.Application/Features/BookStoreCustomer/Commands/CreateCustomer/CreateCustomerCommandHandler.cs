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

      await _bookStoreCustomerRepository.AddCustomer(request.CustomerId, result.Value.Name, request.IsSeller, request.IsBuyer);

      return Result.Ok();
    }
}