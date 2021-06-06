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

namespace GroceryStore.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<DeleteEventCommandHandler> _logger;

        public DeleteEventCommandHandler(IMapper mapper, ICustomerRepository customerRepository, ILogger<DeleteEventCommandHandler> logger)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _logger = logger;
        }



        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var eventtodelete = await _customerRepository.GetIdByAsync(request.Id);
            try
            {
                if (eventtodelete == null)
                {
                    throw new NotFoundException(nameof(Customer), request.Id);
                }

                await _customerRepository.DeleteAsync(eventtodelete);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Unit.Value;
        }
    }
}
