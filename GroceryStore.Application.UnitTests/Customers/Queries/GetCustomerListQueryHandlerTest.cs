using AutoMapper;
using GroceryStore.Application.Contracts.Persistance;
using GroceryStore.Application.Features.Customers.Queries.GetCustomerList;
using GroceryStore.Application.Profiles;
using GroceryStore.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GroceryStore.Application.UnitTests.Customers.Queries
{
    public class GetCustomerListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;

        public GetCustomerListQueryHandlerTest()
        {
            _mockCustomerRepository = RepositoryMock.getCustomersRepository();

            var configprov = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configprov.CreateMapper();
        }

        [Fact]
        public async Task GetCustomerListTest()
        {
            var handler = new GetCustomerListQueryHandler(_mapper, _mockCustomerRepository.Object);

            var result = await handler.Handle(new GetCustomerListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CustomerListVm>>();

            result.Count.ShouldBe(2);
        }
    }
}
