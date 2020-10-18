using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.ServiceApi
{
  public  interface ISearchApiClient
    {

        Task<List<AdvertType>> Search(string keyword);
    }
}
