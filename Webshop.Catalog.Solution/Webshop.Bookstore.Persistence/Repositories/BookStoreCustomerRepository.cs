﻿using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Bookstore.Persistence.Context;
using Webshop.Domain.Common;

namespace Webshop.Bookstore.Persistence.Repositories;

public class BookStoreCustomerRepository : IBookStoreCustomerRepository
{
    private readonly BookstoreDbContext _context;
    public BookStoreCustomerRepository(BookstoreDbContext context)
    {
        _context = context;
    }

    public async Task<Result> AddCustomer(int customerId, string name, bool isSeller, bool isBuyer)
    {
        BookstoreCustomer customer = new()
        {
            Name = name,
            BaseCustomeerId = customerId,
            IsSeller = isSeller,
            IsBuyer = isBuyer
        };

        _context.BookstoreCustomers.Add(customer);
        await _context.SaveChangesAsync();
        return Result.Ok();
    }

    public Task<Result> DeleteCustomer(Guid customerId)
    {
        BookstoreCustomer? customer = _context.BookstoreCustomers.Find(customerId);
        if (customer == null) return Task.FromResult(Result.Fail(Errors.General.NotFound(customerId)));
        _context.BookstoreCustomers.Remove(customer);
        return Task.FromResult(Result.Ok());
    }

    public async Task<Result> UpdateCustomer(Guid customerId, bool isSeller, bool isBuyer)
    {
        BookstoreCustomer? customer = _context.BookstoreCustomers.Find(customerId);
        if (customer == null)
        {
            return Result.Fail(Errors.General.NotFound(customerId));
        }
        customer.IsSeller = isSeller;
        customer.IsBuyer = isBuyer;
        await _context.SaveChangesAsync();
        return Result.Ok();
    }
}