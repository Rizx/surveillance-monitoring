using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;

namespace API.Mapping
{
    public class CameraProfile : Profile
    {
        public CameraProfile()
        {
            CreateMap<Camera, CameraResponse>().ReverseMap();
            CreateMap<Camera, VideoUrlResponse>().ReverseMap();
        }
    }
}