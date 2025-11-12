using AutoMapper;
using E_Commerce.Application.Helpers;
using E_Commerce.Domain.Models;
using E_Commerce.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Commerce.Application.CQRS.OrderManagement
{
    #region DTOs

    public class OrderProductDto
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDto
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public int NumberOfProducts { get; set; }
    }

    #endregion

    #region Create Order Command

    public record CreateOrderCommand : IRequest<ResultViewModel<OrderDto>>
    {
        public int CustomerID { get; set; }
        public List<OrderProductDto> Products { get; set; } = new();
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResultViewModel<OrderDto>>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(
            IRepository<Order> orderRepository,
            IRepository<Customer> customerRepository,
            IRepository<Product> productRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultViewModel<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Validate Customer
            var customer = await _customerRepository.GetByIDAsync(request.CustomerID);
            if (customer == null)
                return ResultViewModel<OrderDto>.Faliure(ErrorCode.NotFound, $"Customer with ID {request.CustomerID} not found.");

            // Validate Products
            if (request.Products == null || !request.Products.Any())
                return ResultViewModel<OrderDto>.Faliure(ErrorCode.InvalidData, "Order must contain at least one product.");

            double totalPrice = 0;
            var orderProducts = new List<OrderProduct>();

            foreach (var p in request.Products)
            {
                var product = await _productRepository.GetByIDAsync(p.ProductID);
                if (product == null)
                    return ResultViewModel<OrderDto>.Faliure(ErrorCode.NotFound, $"Product with ID {p.ProductID} not found.");

                orderProducts.Add(new OrderProduct
                {
                    ProductID = p.ProductID,
                    Quantity = p.Quantity
                });

                totalPrice += product.price * p.Quantity;
            }

            var order = new Order
            {
                CustomerId = request.CustomerID,
                Status = OrderStatus.Pending.ToString(),
                OrderDate = System.DateTime.Now,
                OrderProducts = orderProducts,
                TotalPrice = totalPrice
            };

            _orderRepository.Add(order);
            _orderRepository.SaveChanges();

            var dto = new OrderDto
            {
                OrderID = order.ID,
                CustomerName = customer.Name,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                NumberOfProducts = orderProducts.Count
            };

            return ResultViewModel<OrderDto>.Sucess(dto, "Order created successfully.");
        }
    }

    #endregion

    #region Get Order By ID Query

    public record GetOrderByIdQuery(int OrderId) : IRequest<ResultViewModel<OrderDto>>;

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ResultViewModel<OrderDto>>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ResultViewModel<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository
                .GetAll(o => o.Customer, o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.ID == request.OrderId, cancellationToken);

            if (order == null)
                return ResultViewModel<OrderDto>.Faliure(ErrorCode.NotFound, $"Order with ID {request.OrderId} not found.");

            var dto = _mapper.Map<OrderDto>(order);

            return ResultViewModel<OrderDto>.Sucess(dto, "Order retrieved successfully.");
        }
    }

    #endregion
}
