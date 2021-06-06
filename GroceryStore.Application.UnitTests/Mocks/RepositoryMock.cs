using GroceryStore.Application.Contracts.Persistance;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using GroceryStore.Domain.Entities;

namespace GroceryStore.Application.UnitTests.Mocks
{
    public class RepositoryMock
    {
        public static Mock<ICustomerRepository> getCustomersRepository()
        {
            var cc = new List<Customer>
            {
                new Customer
                {
                    Id =1,
                    Name = "Rinkesh"
                },
                new Customer
                {
                    Id =2,
                    Name = "Jyoti"
                }
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(cc);

            mockCustomerRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(
                (Customer customer) =>
                {
                    cc.Add(customer);
                    return customer;
                });

            return mockCustomerRepository;
        }
    }
}
