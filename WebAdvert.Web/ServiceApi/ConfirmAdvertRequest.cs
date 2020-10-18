using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.ServiceApi
{
    public class ConfirmAdvertRequest
    {
        public string Id { get; set; }
        public AdvertStatus Status { get; set; }

        public string FilePath { get; set; }
    }
}
