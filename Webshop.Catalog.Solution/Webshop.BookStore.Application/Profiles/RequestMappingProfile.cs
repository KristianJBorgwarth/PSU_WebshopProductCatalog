using AutoMapper;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Features.Requests;

namespace Webshop.BookStore.Application.Profiles;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
    }
}