using AutoMapper;
using E_Commerce.Application.Helpers;
using E_Commerce.Domain.Models;
using E_Commerce.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Commerce.Application.CQRS.OrderManagement
{
    #region Update Order Status Command

    public record UpdateOrderStatusCommand(int OrderId) : IRequest<ResultViewModel<OrderDto>>;

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, ResultViewModel<OrderDto>>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public UpdateOrderStatusCommandHandler(
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultViewModel<OrderDto>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository
     .GetAll(o => o.Customer, o => o.OrderProducts)
     .FirstOrDefaultAsync(o => o.ID == request.OrderId, cancellationToken);


            if (order == null)
                return ResultViewModel<OrderDto>.Faliure(ErrorCode.NotFound, $"Order with ID {request.OrderId} not found.");

            // Update status
            order.Status = OrderStatus.Delivered.ToString();

            // Update stock
            foreach (var op in order.OrderProducts)
            {
                var product = await _productRepository.GetByIDAsync(op.ProductID);
                if (product != null)
                {
                    product.stock -= op.Quantity;
                    if (product.stock < 0) product.stock = 0; // Avoid negative stock
                    _productRepository.Update(product);
                }
            }

            _orderRepository.Update(order);
            _orderRepository.SaveChanges();

            var dto = _mapper.Map<OrderDto>(order);

            return ResultViewModel<OrderDto>.Sucess(dto, "Order status updated to Delivered and stock updated.");
        }
    }

    #endregion
}
