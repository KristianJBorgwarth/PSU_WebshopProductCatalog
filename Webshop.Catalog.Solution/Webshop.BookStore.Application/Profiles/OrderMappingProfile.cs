using AutoMapper;
using Webshop.BookStore.Application.Features.Order.Commands.AddOrderItemCommand;
using Webshop.BookStore.Application.Features.Order.Commands.CreateOrder;
using Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;
using Webshop.BookStore.Application.Features.Order.Requests;
using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.BookStore.Application.Profiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrderCommand>();
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<AddOrderItemRequest, AddOrderItemCommand>()
            .ForMember(dest => dest.OrderItem, opt => opt.MapFrom(src => new OrderItem
            {
                BookId = src.BookId,
                BookTitle = src.BookTitle,
                Price = src.Price,
                Quantity = src.Quantity
            }));

        CreateMap<ProcessOrderRequest, ProcessOrderCommand>();
    }
}