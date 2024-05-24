using AutoMapper;
using Webshop.BookStore.Application.Features.Book.Dtos;

namespace Webshop.BookStore.Application.Profiles;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Domain.AggregateRoots.Book, BookDto>().ReverseMap();
    }
}