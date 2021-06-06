using GroceryStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Application.Contracts.Persistance
{
    public interface ICustomerRepository 
    {
        Task<Customer> GetIdByAsync(int id);
        Task<IReadOnlyList<Customer>> ListAllAsync();
        Task<Customer> AddAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task DeleteAsync(Customer entity);
        Task<bool> isCustomerNameUnique(string name);
    }
}
