﻿using Microsoft.EntityFrameworkCore;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Bookstore.Persistence.Context;
using Webshop.Data.Persistence;
using Webshop.Domain.Common;

namespace Webshop.Bookstore.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookstoreDbContext _context;

    public BookRepository(BookstoreDbContext context)
    {
        _context = context;
    }

    public async Task<Book[]> GetBooksByCategory(int categoryId)
    {
        var books = await _context.Books.Where(c => c.CategoryId == categoryId).ToArrayAsync();
        return books;
    }

    public async Task<Book[]> GetBooksBySeller(int sellerId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(Book entity)
    {
        _context.Books.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Book> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Book>> GetAll()
    {
        var books = await _context.Books.ToArrayAsync();
        return books;
    }

    public async Task UpdateAsync(Book entity)
    {
        throw new NotImplementedException();
    }
}