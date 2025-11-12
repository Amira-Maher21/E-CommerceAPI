using AutoMapper;
using E_Commerce.Application.CQRS.CustomerManagement.Queries;
using E_Commerce.Application.CQRS.OrderManagement;
 using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace E_Commerce.Application.Profiles
{
 
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.ID))
                .ReverseMap();

            //// Order
            //CreateMap<Order, OrderDto>()
            //    .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.ID))
            //   //.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.))
            //    .ReverseMap();

            // OrderProduct
            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ID))
                .ReverseMap();



            CreateMap<Order, OrderDto>()
      .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.ID))
      .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
      .ForMember(dest => dest.NumberOfProducts, opt => opt.MapFrom(src => src.OrderProducts.Count));


            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ID))
                .ReverseMap();
        }
    }
}
