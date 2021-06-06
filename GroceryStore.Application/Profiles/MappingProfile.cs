using AutoMapper;
using GroceryStore.Application.Features.Customers;
using GroceryStore.Application.Features.Customers.Commands.CreateCustomer;
using GroceryStore.Application.Features.Customers.Commands.UpdateCustomer;
using GroceryStore.Application.Features.Customers.JsonModel;
using GroceryStore.Application.Features.Customers.Queries.GetCustomerDetail;
using GroceryStore.Application.Features.Customers.Queries.GetCustomerList;
using GroceryStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerListVm>().ReverseMap();
            CreateMap<Customer, CustomerDetailVm>().ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<JsonCustomer,Customer>().ReverseMap();
            CreateMap<JsonCustomersData, List<JsonCustomer>>().ReverseMap()
                .ForMember(x=> x.Customers , o=> o.MapFrom(s=> s));
                
           
        }
    }
}
