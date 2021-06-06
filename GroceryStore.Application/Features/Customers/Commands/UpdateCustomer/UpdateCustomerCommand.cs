using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
