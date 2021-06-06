using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Application.Features.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery : IRequest<CustomerDetailVm>
    {
        public int Id { get; set; }
    }
}
