using AutoMapper;
using GroceryStore.Application.Contracts.Persistance;
using GroceryStore.Application.Features.Customers.JsonModel;
using GroceryStore.Application.Profiles;
using GroceryStore.Domain.Entities;
using GroceryStrore.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GroceryStore.Persistence.IntegrationTests
{
    public class GroceryStoreDbContextTests 
    {
        private IMapper _mapper;              
        public IConfiguration Configuration { get; set; }

        public Mock<ILogger<GroceryStoreJsonDbContext>> _logger;
        public GroceryStoreDbContextTests()
        {
            var configprov = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _logger = new Mock<ILogger<GroceryStoreJsonDbContext>>();
            _mapper = configprov.CreateMapper();
            Configuration = GetIConfigurationRoot("appsettings.json");           
        }

        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

        [Fact]
        public async void Initializedbfromjson()
        {
            GroceryStoreJsonDbContext _groceryStoreJsonDbContext = 
                new GroceryStoreJsonDbContext(Configuration, _mapper, _logger.Object);
           var Jsoncustomerdata=  await _groceryStoreJsonDbContext.initializedatafromdbAsync<JsonCustomersData>();            

            Assert.NotEmpty(Jsoncustomerdata.Customers);
       
        }        
    }   
}
