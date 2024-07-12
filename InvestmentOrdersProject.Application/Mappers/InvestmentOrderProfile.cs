using AutoMapper;
using InvestmentOrdersProject.Application.DTOs;
using InvestmentOrdersProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Application.Mappers
{
    public class InvestmentOrderProfile : Profile
    {
        public InvestmentOrderProfile()
        {
            CreateMap<InvestmentOrder, InvestmentOrderDto>().ReverseMap();
            
            CreateMap<InvestmentOrderCreateDto, InvestmentOrder>();
        }
    }
}
