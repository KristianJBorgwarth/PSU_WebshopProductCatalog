using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webshop.Customer.Api.Controllers;

namespace Webshop.BookStore.Api.Controllers;

public class BookController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookController> _logger;
    private readonly IMapper _mapper;

    public BookController(IMediator mediator, ILogger<BookController> logger, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }
}