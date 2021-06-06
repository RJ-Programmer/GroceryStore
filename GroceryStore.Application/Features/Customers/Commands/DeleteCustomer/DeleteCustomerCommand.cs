using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public int Id { get; set; }
      
    }
}
