using AutoMapper;
using GroceryStore.Application.Contracts.Persistance;
using GroceryStore.Application.Exceptions;
using GroceryStore.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStore.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;

        public UpdateCustomerCommandHandler(IMapper mapper, ICustomerRepository customerRepository, ILogger<UpdateCustomerCommandHandler> logger)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var eventtoupdate = await _customerRepository.GetIdByAsync(request.Id);
            try
            {
                if (eventtoupdate == null)
                {
                    throw new NotFoundException(nameof(Customer), request.Id);
                }

                _mapper.Map(request, eventtoupdate, typeof(UpdateCustomerCommand), typeof(Customer));

                await _customerRepository.UpdateAsync(eventtoupdate);
            }
            catch (NotFoundException)
            {
                throw ;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
            }

            return Unit.Value;
        }
    }
}
