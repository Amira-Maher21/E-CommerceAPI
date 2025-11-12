using E_Commerce.Application.Helpers;
using E_Commerce.Application;
using E_Commerce.Domain.Models;
using E_Commerce.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace E_Commerce.Application.CQRS.OrderManagement.Queries
{
    // الطلب حسب الـ ID
    public record GETOrderByIDQuery(int Id) : IRequest<ResultViewModel<OrderDto>>;

    public class OrderDto
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public int NumberOfProducts { get; set; }
    }

    public class GETOrderByIDQueryHandler
        : IRequestHandler<GETOrderByIDQuery, ResultViewModel<OrderDto>>
    {
        private readonly IRepository<Order> _orderRepository;

        public GETOrderByIDQueryHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResultViewModel<OrderDto>> Handle(GETOrderByIDQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIDAsync(request.Id);

            if (order == null)
            {
                return ResultViewModel<OrderDto>.Faliure(
                    ErrorCode.NotFound,
                    $"Order with ID {request.Id} not found."
                );
            }

            // التحويل لـ DTO
            var dto = new OrderDto
            {
                OrderID = order.ID,
                CustomerName = order.Customer?.Name ?? "Unknown",
                Status = order.Status,
                NumberOfProducts = order.OrderProducts?.Count ?? 0
            };

            return ResultViewModel<OrderDto>.Sucess(dto, "Order retrieved successfully.");
        }
    }
}
