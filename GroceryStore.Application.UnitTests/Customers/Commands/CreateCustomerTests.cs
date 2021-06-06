using AutoMapper;
using GroceryStore.Application.Contracts.Persistance;
using GroceryStore.Application.Features.Customers.Commands.CreateCustomer;
using GroceryStore.Application.Profiles;
using GroceryStore.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GroceryStore.Application.UnitTests.Customers.Commands
{
    public class CreateCustomerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;

        private readonly Mock<ILogger<CreateCustomerCommandHandler>> _mockLogging;
        public CreateCustomerTests()
        {
            _mockCustomerRepository = RepositoryMock.getCustomersRepository();

            var configprov = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configprov.CreateMapper();
            _mockLogging = new Mock<ILogger<CreateCustomerCommandHandler>>();
        }

        [Fact]
        public async Task CreateCustomerTest()
        {
            var handler = new CreateCustomerCommandHandler(_mapper, _mockCustomerRepository.Object, _mockLogging.Object);

            await handler.Handle(new CreateCustomerCommand() { Name = "Test" }, CancellationToken.None);

            var allCategories = await _mockCustomerRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBeGreaterThan(0);
        }
    }
}
