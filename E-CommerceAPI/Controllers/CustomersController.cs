using MediatR;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Application.CQRS.CustomerManagement.Queries;
using E_Commerce.Shared;
using E_Commerce.Application.CQRS.CustomerManagement.Commands;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ResultViewModel<CustomerDto>> GetCustomerById(int id)
    {
        return await _mediator.Send(new GETCustomersByIDQuery(id));
    }

    [HttpGet]
    public async Task<ResultViewModel<IEnumerable<CustomerDto>>> GetAllCustomers()
    {
        return await _mediator.Send(new GETAllCustomersQuery());
    }
    [HttpPost]
    public async Task<ResultViewModel<CustomerDto>> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        return await _mediator.Send(command);
    }

}
