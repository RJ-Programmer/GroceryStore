using GroceryStore.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Application.Features.Customers.Commands.CreateCustomer
{
   public class CreateCustomerCommandResponse : BaseResponse
    {
        public CreateCustomerCommandResponse() : base()
        {

        }

        public CreateCustomerDto Customer { get; set; }
    }
}
