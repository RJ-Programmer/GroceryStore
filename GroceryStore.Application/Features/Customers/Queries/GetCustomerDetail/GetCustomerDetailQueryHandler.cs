using AutoMapper;
using GroceryStore.Application.Contracts.Persistance;
using GroceryStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStore.Application.Features.Customers.Queries.GetCustomerDetail
{
    //this class is created assuming customer will have more dependency like address ,contact etc , i havent added as this is out of scope
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailVm>
    {
        //hear you can add new Address repo and inject in contructor as well 
        private readonly IMapper _mapper;
        
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerDetailQueryHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }
        public async Task<CustomerDetailVm> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetIdByAsync(request.Id);

            var customerdetaildto = _mapper.Map<CustomerDetailVm>(customer);

            //here you can map more entity like customerdetaildto.address 
            return customerdetaildto;
        }
    }
}
