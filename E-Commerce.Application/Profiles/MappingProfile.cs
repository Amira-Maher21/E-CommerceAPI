using AutoMapper;
using E_Commerce.Application.CQRS.CustomerManagement.Queries;
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
            CreateMap<CustomerDto, Customer>()

            .ReverseMap()
             .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.ID));

        }
    }
}
