using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;

namespace API.Mapping
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            CreateMap<History, HistoryResponse>().ReverseMap()
            .ForMember(x=>x.Date, opt => opt.MapFrom(src => src.Date));
        }
    }
}