using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
namespace WebAdvert.Web.ServiceApi
{
    public interface IAdvertApiClient
    {
        Task<CreateAdvertResponse> Create(CreateAdvertModel models);

        Task<bool> Confirm(ConfirmAdvertRequest confirmAdvertRequest);

    }
}
