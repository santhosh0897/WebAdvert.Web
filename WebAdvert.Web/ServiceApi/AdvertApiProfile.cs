using AdvertApi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.ServiceApi
{
    public class AdvertApiProfile : Profile
    {
        public AdvertApiProfile()
        {
            CreateMap<AdvertModels, CreateAdvertModel>().ReverseMap();
            CreateMap<CreateAdvertResponse, AdvertResponse>().ReverseMap();
            CreateMap<ConfirmAdvertModel, ConfirmAdvertRequest>().ReverseMap();

        }
    }
}
