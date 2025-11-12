//using E_Commerce.Application.Repositories;
//using E_Commerce.Domain.Exceptions;
//using E_Commerce.Domain.Models;
//  using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace E_Commerce.Application.CQRS.CustomerManagement.Queries
//{
//    public record GETCustomersByIDQuery(int id):IRequest<CustomerDto>
//    {
//        public int ID => id; 
//    }
//    public class CustomerDto()
//    {
//        public int ID { get; set; } 
//        public string Name { get; set; }

//        public string Email { get; set; }

//        public string Phone { get; set; }
//    }
//    public class GETCustomersByIDQueryHandler : IRequestHandler<GETCustomersByIDQuery, CustomerDto>
//    {
//        IRepository<Customer> _CustomerRepository;
//        public GETCustomersByIDQueryHandler (IRepository<Customer> repository)
//        {
//            _CustomerRepository = repository;
//        }

//        public async Task<CustomerDto> Handle(GETCustomersByIDQuery request, CancellationToken cancellationToken)
//        {
//            var customer = await _CustomerRepository.GetByIDAsync(request.id);

//            if (customer == null)
//                throw new NotFoundException($"Customer with ID {request.id} not found.");

//            return new CustomerDto
//            {
//                ID = customer.ID,
//                Name = customer.Name,
//                Email = customer.Email,
//                Phone = customer.Phone
//            };
//        }

//        Task<CustomerDto> IRequestHandler<GETCustomersByIDQuery, CustomerDto>.Handle(GETCustomersByIDQuery request, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }
//    }


//}

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
}

