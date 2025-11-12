using E_Commerce.Application.CQRS.OrderManagement;
  using E_Commerce.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
 
namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET /orders/{id}
        [HttpGet("{id}")]
        public async Task<ResultViewModel<OrderDto>> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery(id); // ✅ الاسم الصحيح
            return await _mediator.Send(query);
        }

    }
}
