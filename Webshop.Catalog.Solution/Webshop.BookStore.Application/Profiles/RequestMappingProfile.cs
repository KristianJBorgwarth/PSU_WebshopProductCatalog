using AutoMapper;
using Webshop.BookStore.Application.Features.Book.Commands.CreateBook;
using Webshop.BookStore.Application.Features.Book.Commands.UpdateBook;
using Webshop.BookStore.Application.Features.Book.Requests;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Requests;
using Webshop.BookStore.Application.Features.Order.Commands.CreateOrder;
using Webshop.BookStore.Application.Features.Order.Requests;

namespace Webshop.BookStore.Application.Profiles;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        // Customer
        CreateMap<CreateBookStoreCustomerRequest, CreateBookStoreCustomerCommand>();
        CreateMap<UpdateBookStoreCustomerRequest, UpdateBookStoreCustomerCommand>();

        // Book
        CreateMap<CreateBookRequest, CreateBookCommand>();
        CreateMap<UpdateBookRequest, UpdateBookCommand>();
    }
}