using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;

namespace API.Mapping
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberRequest>().ReverseMap();
            CreateMap<Member, MemberResponse>().ReverseMap();
            CreateMap<Member, UserResponse>().ReverseMap();
        }
    }
}