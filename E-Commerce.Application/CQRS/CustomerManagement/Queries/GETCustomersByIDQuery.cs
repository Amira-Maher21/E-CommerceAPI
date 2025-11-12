

using E_Commerce.Application.Helpers;
using E_Commerce.Application;
using E_Commerce.Domain.Models;
using E_Commerce.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Commerce.Application.CQRS.CustomerManagement.Queries
{
     public record GETCustomersByIDQuery(int Id) : IRequest<ResultViewModel<CustomerDto>>;

    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class GETCustomersByIDQueryHandler
        : IRequestHandler<GETCustomersByIDQuery, ResultViewModel<CustomerDto>>
    {
        private readonly IRepository<Customer> _customerRepository;

        public GETCustomersByIDQueryHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResultViewModel<CustomerDto>> Handle(GETCustomersByIDQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIDAsync(request.Id);

            if (customer == null)
            {
                return ResultViewModel<CustomerDto>.Faliure(
                    ErrorCode.NotFound,
                    $"Customer with ID {request.Id} not found."
                );
            }

             var dto = customer.MapOne<CustomerDto>();

            return ResultViewModel<CustomerDto>.Sucess(dto, "Customer retrieved successfully.");
        }
    }




  
         public record GETAllCustomersQuery() : IRequest<ResultViewModel<IEnumerable<CustomerDto>>>;

        public class GETAllCustomersQueryHandler
            : IRequestHandler<GETAllCustomersQuery, ResultViewModel<IEnumerable<CustomerDto>>>
        {
            private readonly IRepository<Customer> _customerRepository;

            public GETAllCustomersQueryHandler(IRepository<Customer> customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<ResultViewModel<IEnumerable<CustomerDto>>> Handle(GETAllCustomersQuery request, CancellationToken cancellationToken)
            {
                var customers = _customerRepository.GetAll(); // IQueryable<Customer>

                if (customers == null)
                {
                    return ResultViewModel<IEnumerable<CustomerDto>>.Faliure(
                        ErrorCode.NotFound,
                        "No customers found."
                    );
                }

                // تحويل للقائمة DTOs
                var dtoList = customers.Map<CustomerDto>(); // MapperHelper مع AutoMapper

                return ResultViewModel<IEnumerable<CustomerDto>>.Sucess(dtoList, "Customers retrieved successfully.");
            }
        }
    }


