using AutoMapper;
using Webshop.BookStore.Application.Features.Order.Commands.CreateOrder;
using Webshop.BookStore.Application.Features.Order.Requests;
using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.BookStore.Application.Profiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrderCommand>();
        CreateMap<CreateOrderCommand, Order>();
    }
}