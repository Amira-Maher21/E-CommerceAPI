using E_Commerce.Application.CQRS.CustomerManagement.Queries;
using E_Commerce.Application.Helpers;
using E_Commerce.Domain.Models;
using E_Commerce.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_Commerce.Application.CQRS.CustomerManagement.Commands
{
    public class CreateCustomerCommandHandler
       : IRequestHandler<CreateCustomerCommand, ResultViewModel<CustomerDto>>
    {
        private readonly IRepository<Customer> _customerRepository;

        public CreateCustomerCommandHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResultViewModel<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(request.Name))
                return ResultViewModel<CustomerDto>.Faliure(ErrorCode.BadRequest, "Customer name is required.");

            if (string.IsNullOrWhiteSpace(request.Email))
                return ResultViewModel<CustomerDto>.Faliure(ErrorCode.BadRequest, "Email is required.");

            // Basic email format validation
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(request.Email, emailPattern))
                return ResultViewModel<CustomerDto>.Faliure(ErrorCode.BadRequest, "Invalid email format.");

            // Create Customer
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();

            var dto = customer.MapOne<CustomerDto>();

            return ResultViewModel<CustomerDto>.Sucess(dto, "Customer created successfully.");
        }
    }
    public class CreateCustomerCommand : IRequest<ResultViewModel<CustomerDto>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}


