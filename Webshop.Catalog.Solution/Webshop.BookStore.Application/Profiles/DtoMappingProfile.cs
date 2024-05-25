using AutoMapper;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Dtos;

namespace Webshop.BookStore.Application.Profiles;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Domain.AggregateRoots.Book, BookDto>().ReverseMap();
        CreateMap<Domain.AggregateRoots.BookstoreCustomer, BookStoreCustomerDto>().ReverseMap();
    }
}