using GroceryStore.Application.Contracts.Persistance;
using GroceryStore.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Persistence
{
    public static class PersistanceServiceRegistration 
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<GroceryStoreJsonDbContext, GroceryStoreJsonDbContext>();                       
            services.AddScoped<ICustomerRepository, CustomerRepository>();           

            return services;
        }
    }
}
