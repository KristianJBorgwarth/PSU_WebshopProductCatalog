using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooksByCategory;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Dtos;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomerById;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomers;
using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.Bookstore.Application.Test.Unit.QueryHandlerTests.BookStoreCustomer
{
    public class GetBookStoreCustomerByIdQueryHandlerTests
    {
        private readonly GetBookStoreCustomerByIdQueryHandler _handler;
        private readonly IBookStoreCustomerRepository _fakeCustomerRepository;
        private readonly IMapper _fakeMapper;

        public GetBookStoreCustomerByIdQueryHandlerTests()
        {
            _fakeCustomerRepository = A.Fake<IBookStoreCustomerRepository>();
            _fakeMapper = A.Fake<IMapper>();
            _handler = new GetBookStoreCustomerByIdQueryHandler(_fakeCustomerRepository, _fakeMapper);
        }
        [Fact]
        public async void GivenValidQuery_ShouldReturn_OKandExactlyOneCustomer()
        {
            //Arrange
            var query = new GetBookStoreCustomerByIdQuery { id = 1 };
            var bookStoreCustomer = new BookstoreCustomer()
            {
                Name = "Test",
                BaseCustomeerId = 1,
                IsBuyer = true,
                IsSeller = true,
                Id = 1
            };
            var bookStoreCustomerDto = new BookStoreCustomerDto()
            {
                Name = "Test",
                BaseCustomeerId = 1,
                IsBuyer = true,
                IsSeller = true,
                Id = 1
            };

            A.CallTo(() => _fakeCustomerRepository.GetById(query.id)).Returns(Task.FromResult(bookStoreCustomer));
            A.CallTo(() => _fakeMapper.Map<BookStoreCustomerDto>(bookStoreCustomer)).Returns(bookStoreCustomerDto);

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Success.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(bookStoreCustomerDto);
            A.CallTo(() => _fakeCustomerRepository.GetById(query.id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeMapper.Map<BookStoreCustomerDto>(bookStoreCustomer)).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async void Handle_GivenNonExistentCustomer_ShouldReturn_NotFound()
        {
            //Arrange
            var query = new GetBookStoreCustomerByIdQuery { id = 999999999 };
            var exceptionsMessage = "Customer Not found";

            A.CallTo(() => _fakeCustomerRepository.GetById(query.id)).Throws( new Exception(exceptionsMessage));

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain(exceptionsMessage);
            A.CallTo(() => _fakeCustomerRepository.GetById(query.id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeMapper.Map<BookStoreCustomerDto>(A<BookstoreCustomer>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async void Handle_GivenInvalidId_ShouldReturn_BadRequest()
        {
            //Arrange
            var query = new GetBookStoreCustomerByIdQuery { id = -1 };
            var exceptionsMessage = "Invaild customerId";

            A.CallTo(() => _fakeCustomerRepository.GetById(query.id)).Throws(new Exception(exceptionsMessage));

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain(exceptionsMessage);
            A.CallTo(() => _fakeCustomerRepository.GetById(query.id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeMapper.Map<BookStoreCustomerDto>(A<BookstoreCustomer>.Ignored)).MustNotHaveHappened();
        }
    }
    
}
