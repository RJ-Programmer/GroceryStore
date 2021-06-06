using AutoMapper;
using GroceryStore.Application.Contracts.Persistance;
using GroceryStore.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStore.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;

        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(IMapper mapper,ICustomerRepository customerRepository, ILogger<CreateCustomerCommandHandler> logger)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        //public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        //{
        //    var validator = new CreateCustomerCommandValidator();
        //    var validationResult = await validator.ValidateAsync(request);

        //    if (validationResult.Errors.Count > 0)
        //        throw new Exceptions.ValidationException(validationResult);


        //    var @customer = _mapper.Map<Customer>(request);

        //    @customer = await _customerRepository.AddAsync(@customer);

        //    return @customer.Id;
            
        //}

        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var createCustomerCommandResponse = new CreateCustomerCommandResponse();

            try
            {
                var validator = new CreateCustomerCommandValidator(_customerRepository);
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult.Errors.Count > 0)
                {
                    createCustomerCommandResponse.Success = false;
                    createCustomerCommandResponse.ValidationErrors = new List<string>();

                    foreach (var error in validationResult.Errors)
                    {
                        createCustomerCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                    }

                }

                if (createCustomerCommandResponse.Success)
                {
                    var @customer = new Customer() { Name = request.Name };
                    @customer = await _customerRepository.AddAsync(@customer);
                    createCustomerCommandResponse.Customer = _mapper.Map<CreateCustomerDto>(@customer);

                    _logger.LogInformation("Customer created" + @customer.Id);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }

            
            //    throw new Exceptions.ValidationException(validationResult);            

            return createCustomerCommandResponse;

        }
    }
}
