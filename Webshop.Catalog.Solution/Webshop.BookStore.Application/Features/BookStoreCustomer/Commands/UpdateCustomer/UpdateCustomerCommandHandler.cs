﻿using MediatR;
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
            try
            {
                var bc = await _bookStoreCustomerRepository.GetById(request.Id);
                if (bc == null) return Result.Fail(Errors.General.NotFound(request.Id));

                bc.IsBuyer = request.IsBuyer;
                bc.IsSeller = request.IsSeller;

                await _bookStoreCustomerRepository.UpdateAsync(bc);
                return Result.Ok();
            }
            catch(Exception ex)
            {
                return Result.Fail(Errors.General.UnspecifiedError(ex.Message));
            }
        }
    }
}
