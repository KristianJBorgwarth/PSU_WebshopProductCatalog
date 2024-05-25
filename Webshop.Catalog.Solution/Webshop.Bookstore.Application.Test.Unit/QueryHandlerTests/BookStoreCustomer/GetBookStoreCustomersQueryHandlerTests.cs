using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Dtos;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Queries.GetBookStoreCustomers;
using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.Bookstore.Application.Test.Unit.QueryHandlerTests.BookStoreCustomer
{
    public class GetBookStoreCustomersQueryHandlerTests
    {
        private readonly GetBookStoreCustomersQueryHandler _handler;
        private readonly IBookStoreCustomerRepository _fakeCustomerRepository;
        private readonly IMapper _fakeMapper;

        public GetBookStoreCustomersQueryHandlerTests()
        {
            _fakeCustomerRepository = A.Fake<IBookStoreCustomerRepository>();
            _fakeMapper = A.Fake<IMapper>();
            _handler = new GetBookStoreCustomersQueryHandler(_fakeCustomerRepository, _fakeMapper);
        }

        [Fact]
        public async void GivenValidQuery_ShouldReturn_OKandListOfCustomers()
        {
            //Arrange
            List<BookstoreCustomer> customers = new List<BookstoreCustomer>()
            {
                new BookstoreCustomer() {
                    Name = "Test",
                    BaseCustomeerId = 1,
                    IsBuyer = true,
                    IsSeller = true,
                    Id = 1
                    },
                new BookstoreCustomer() {
                    Name = "Test",
                    BaseCustomeerId = 2,
                    IsBuyer = true,
                    IsSeller = true,
                    Id = 2
                    }
            };
            List<BookStoreCustomerDto> bookStoreCustomerDtos = new List<BookStoreCustomerDto>
            {
                new BookStoreCustomerDto
                {
                    Name = "Test",
                    BaseCustomeerId = 1,
                    IsBuyer = true,
                    IsSeller = true,
                    Id = 1
                },
                new BookStoreCustomerDto
                {
                    Name = "Test",
                    BaseCustomeerId = 2,
                    IsBuyer = true,
                    IsSeller = true,
                    Id = 2
                }
            };
            A.CallTo(() => _fakeCustomerRepository.GetAll()).Returns(customers);
            A.CallTo(() => _fakeMapper.Map<List<BookStoreCustomerDto>>(customers)).Returns(bookStoreCustomerDtos);
            //Act
            var result = await _handler.Handle(new GetBookStoreCustomersQuery(), CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(bookStoreCustomerDtos);
            A.CallTo(() => _fakeCustomerRepository.GetAll()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeMapper.Map<List<BookStoreCustomerDto>>(customers)).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async void GivenRepositoryThrowsException_ShouldReturn_FailResult()
        {
            // Arrange
            var exceptionMessage = "Repository exception";
            A.CallTo(() => _fakeCustomerRepository.GetAll()).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _handler.Handle(new GetBookStoreCustomersQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Be(exceptionMessage);
            A.CallTo(() => _fakeCustomerRepository.GetAll()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeMapper.Map<List<BookStoreCustomerDto>>(A<List<BookstoreCustomer>>._)).MustNotHaveHappened();
        }
        [Fact]
        public async void GivenEmptyResult_ShouldReturn_OKandEmptyList()
        {
            // Arrange
            List<BookstoreCustomer> emptyCustomers = new List<BookstoreCustomer>();
            List<BookStoreCustomerDto> emptyCustomerDtos = new List<BookStoreCustomerDto>();
            A.CallTo(() => _fakeCustomerRepository.GetAll()).Returns(emptyCustomers);
            A.CallTo(() => _fakeMapper.Map<List<BookStoreCustomerDto>>(emptyCustomers)).Returns(emptyCustomerDtos);

            // Act
            var result = await _handler.Handle(new GetBookStoreCustomersQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Value.Should().BeEmpty();
            A.CallTo(() => _fakeCustomerRepository.GetAll()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeMapper.Map<List<BookStoreCustomerDto>>(emptyCustomers)).MustHaveHappenedOnceExactly();
        }
    }
}
