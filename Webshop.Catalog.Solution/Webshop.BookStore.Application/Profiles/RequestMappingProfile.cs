using AutoMapper;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Commands.CreateBook;
using Webshop.BookStore.Application.Features.Book.Requests;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;
using Webshop.BookStore.Application.Features.Requests;

namespace Webshop.BookStore.Application.Profiles;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
        CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>();
        CreateMap<DeleteCustomerRequest, DeleteCustomerCommand>();
        CreateMap<CreateBookRequest, CreateBookCommand>();
    }
}