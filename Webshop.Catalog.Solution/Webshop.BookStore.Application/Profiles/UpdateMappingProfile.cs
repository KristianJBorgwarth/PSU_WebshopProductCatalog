using AutoMapper;
using Webshop.BookStore.Application.Features.Book.Commands.UpdateBook;
using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.BookStore.Application.Profiles;

public class UpdateMappingProfile : Profile
{
    public UpdateMappingProfile ()
    {
        CreateMap<UpdateBookCommand, Book>();
    }
}