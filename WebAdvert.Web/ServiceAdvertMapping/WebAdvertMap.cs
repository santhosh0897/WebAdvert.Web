using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.ServiceApi;

namespace WebAdvert.Web.ServiceAdvertMapping
{
    public class WebAdvertMap : Profile
    {
        public WebAdvertMap()
        {
            CreateMap<CreateAdvertViewModel, CreateAdvertModel>().ReverseMap();
        }
    }
}
