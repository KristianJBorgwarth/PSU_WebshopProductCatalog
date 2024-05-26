using AutoMapper;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Commands.CreateBook;
using Webshop.BookStore.Application.Features.Book.Commands.UpdateBook;
using Webshop.BookStore.Application.Features.Book.Queries.GetBook;
using Webshop.BookStore.Application.Features.Book.Requests;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Requests;

namespace Webshop.BookStore.Application.Profiles;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        // Customer
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
        CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>();
        CreateMap<DeleteCustomerRequest, DeleteCustomerCommand>();

        // Book
        CreateMap<CreateBookRequest, CreateBookCommand>();
        CreateMap<UpdateBookRequest, UpdateBookCommand>();
    }
}