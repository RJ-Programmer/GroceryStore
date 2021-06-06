using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Application.Features.Customers.Queries.GetCustomerList
{
    public class GetCustomerListQuery : IRequest<List<CustomerListVm>>
    {

    }
}
