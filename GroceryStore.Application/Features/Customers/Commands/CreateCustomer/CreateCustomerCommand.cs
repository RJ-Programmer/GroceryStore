using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Customer Name: {Name} ";
        }
    }
}
