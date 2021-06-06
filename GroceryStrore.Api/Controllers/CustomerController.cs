using GroceryStore.Application.Features.Customers.Commands.CreateCustomer;
using GroceryStore.Application.Features.Customers.Commands.DeleteCustomer;
using GroceryStore.Application.Features.Customers.Commands.UpdateCustomer;
using GroceryStore.Application.Features.Customers.Queries.GetCustomerDetail;
using GroceryStore.Application.Features.Customers.Queries.GetCustomerList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStrore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all",Name ="GetAllCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CustomerListVm>>> GetAllCustomers()
        {
            var dto = await _mediator.Send(new GetCustomerListQuery());

            return Ok(dto);
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CustomerDetailVm>>> GetCustomerById(int id)
        {
            var getcustomerbyidQuery = new GetCustomerDetailQuery() { Id = id };
            var response = await _mediator.Send(getcustomerbyidQuery);

            return Ok(response);
        }

        [HttpPost(Name = "AddCustomer")]        
        public async Task<ActionResult<CreateCustomerCommandResponse>> Create([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            var responce = await _mediator.Send(createCustomerCommand);

            return Ok(responce);
        }

        [HttpPut(Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateCustomerCommand updateCustomerCommand)
        {
            await _mediator.Send(updateCustomerCommand);
            return NoContent();
        }

        [HttpDelete("{id}",Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteCustomerCommand = new DeleteCustomerCommand() { Id = id };
            await _mediator.Send(deleteCustomerCommand);

            return NoContent();
        }

    }
}
