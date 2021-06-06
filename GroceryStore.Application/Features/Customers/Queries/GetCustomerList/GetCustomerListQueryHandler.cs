using AutoMapper;
using GroceryStore.Application.Contracts.Persistance;
using GroceryStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStore.Application.Features.Customers.Queries.GetCustomerList
{
    public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, List<CustomerListVm>>
    {

        private readonly IMapper _mapper;

       
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerListQueryHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }
        public async Task<List<CustomerListVm>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            var allCustomers = (await _customerRepository.ListAllAsync()).OrderBy(x => x.Id);

            return _mapper.Map<List<CustomerListVm>>(allCustomers);
        }
    }
}
