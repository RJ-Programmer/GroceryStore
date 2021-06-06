using AutoMapper;
using GroceryStore.Application.Features.Customers.JsonModel;
using GroceryStore.Application.Features.Customers.Queries.GetCustomerList;
using GroceryStore.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GroceryStore.Persistence
{
    public class GroceryStoreJsonDbContext //: IStoreDBContext
    {
        private readonly IMapper _mapper;

        private readonly ILogger<GroceryStoreJsonDbContext> _logger;
        
        private const string JsonDbFile = "jsondbfilepath";
        public string FilePath
        {
            get
            {
                return Configuration.GetSection(JsonDbFile).Value;
            }
        }
        public IConfiguration Configuration { get; }
        public List<Customer> customers { get; set; }
       
        public GroceryStoreJsonDbContext(IConfiguration configuration, IMapper mapper, ILogger<GroceryStoreJsonDbContext> logger)
        {
            Configuration = configuration;
            _mapper = mapper;
            _logger = logger;

            LoadAllEntitiesinMemory();            
        }

        public void LoadAllEntitiesinMemory()
        {
            var jsonlistcusomer = initializedatafromdbAsync<JsonCustomersData>().Result.Customers as List<JsonCustomer>;
            var customerdetaildto = _mapper.Map<List<Customer>>(jsonlistcusomer);
            customers = customerdetaildto;
        }

        public async Task<T> initializedatafromdbAsync<T>() where T: class
        {
            try
            {
                using FileStream openStream = File.OpenRead(Configuration.GetSection(JsonDbFile).Value);
                var data = await JsonSerializer.DeserializeAsync<T>(openStream,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                var jsondata = data as T;

                return jsondata;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        public async Task<bool> SaveChangesAsync() 
        {
            var customerdetaildto = _mapper.Map<List<JsonCustomer>>(customers);

            var jsonCustomersData = _mapper.Map<JsonCustomersData>(customerdetaildto);

            var Sucess = await SaveChangesinJsonFileAsync<JsonCustomersData>(jsonCustomersData);

            return Sucess;
        }


        public async Task<bool> SaveChangesinJsonFileAsync<T>(object allcustomerdata) where T: class
        {
            try
            {              

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                using var outputStream = File.Create(Configuration.GetSection(JsonDbFile).Value);
                await JsonSerializer.SerializeAsync<T>(
                    outputStream,
                    allcustomerdata as T, options);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
               
    }
    
}
