using FluentValidation;
using GroceryStore.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStore.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

            RuleFor(p => p)
                .MustAsync(CustomerNameUnique)
                .WithMessage("Customer with Same Name exists");
        }

        private async Task<bool> CustomerNameUnique(CreateCustomerCommand arg1, CancellationToken arg2)
        {
            return !(await _customerRepository.isCustomerNameUnique(arg1.Name));            
        }
    }
}
