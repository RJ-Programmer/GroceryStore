using AutoMapper;
using GroceryStore.Application.Contracts.Persistance;
using GroceryStore.Application.Features.Customers.JsonModel;
using GroceryStore.Application.Features.Customers.Queries.GetCustomerList;
using GroceryStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly GroceryStoreJsonDbContext _dbContext;

        //protected readonly StoreDBContext _storedbContext;

        private readonly IMapper _mapper;
        public CustomerRepository(GroceryStoreJsonDbContext groceryStoreDbContext, IMapper mapper)
        {
            _dbContext = groceryStoreDbContext;            
            _mapper = mapper;

        }
        public List<Customer> customers { get; set; }      

        public Task<Customer> GetIdByAsync(int id)
        {          
            return Task.FromResult(_dbContext.customers.SingleOrDefault(r => r.Id == id));
        }     

        public  Task<IReadOnlyList<Customer>> ListAllAsync()
        {
           
            return Task.FromResult((IReadOnlyList<Customer>) _dbContext.customers);
        }

        public async Task<Customer> AddAsync(Customer newcustomer)
        {
            _dbContext.customers.Add(newcustomer);
            newcustomer.Id = _dbContext.customers.Max(r => r.Id) + 1;

            await _dbContext.SaveChangesAsync();

            return newcustomer;
        }

        public async Task UpdateAsync(Customer updatedcustomer)
        {
            var customer = _dbContext.customers.SingleOrDefault(r => r.Id == updatedcustomer.Id);

            if (customer != null)
            {
                customer.Name = updatedcustomer.Name;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer entity)
        {
            var customer = _dbContext.customers.FirstOrDefault(r => r.Id == entity.Id);
            if (customer != null)
            {
                _dbContext.customers.Remove(customer);
            }
            await _dbContext.SaveChangesAsync();
        }       

        public Task<bool> isCustomerNameUnique(string name)
        {            
            var matches = _dbContext.customers.Any(e => e.Name.Equals(name));

            return Task.FromResult(matches);

        }
    }
}
