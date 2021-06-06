using GroceryStore.API.IntegrationTests.Base;
using GroceryStore.Application.Features.Customers.Commands.CreateCustomer;
using GroceryStore.Application.Features.Customers.Queries.GetCustomerDetail;
using GroceryStore.Application.Features.Customers.Queries.GetCustomerList;
using GroceryStore.Domain.Entities;
using GroceryStrore.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GroceryStore.API.IntegrationTests.Controllers
{
    public class CustomerControllerTest : IClassFixture<CustomWeApplicationFactory<Startup>>
    {
        private readonly CustomWeApplicationFactory<Startup> _factory;

        public CustomerControllerTest(CustomWeApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task GetAllCustomersTest()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/customer/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<CustomerListVm>>(responseString);

            Assert.IsType<List<CustomerListVm>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetCustomerByIdTest()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/customer/1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CustomerDetailVm>(responseString);

            Assert.IsType<CustomerDetailVm>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddCustomerTest()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.PostAsync("/api/customer",
                new JsonContent(new Customer() { Name = "FromTest1" }));

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateCustomerCommandResponse>(responseString);                       

            Assert.IsType<CreateCustomerCommandResponse>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCustomerTest()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.PutAsync("/api/customer",
                new JsonContent(new Customer() { Id=1, Name = "FromTest1" }));

            response.EnsureSuccessStatusCode();
           
        }

        [Fact]
        public async Task DeleteCustomerTest()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.DeleteAsync("/api/customer/1");

            response.EnsureSuccessStatusCode();          

        }

    }
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }
}
