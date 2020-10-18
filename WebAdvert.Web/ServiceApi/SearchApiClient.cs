using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;


using System.Threading.Tasks;

namespace WebAdvert.Web.ServiceApi
{
    public class SearchApiClient : ISearchApiClient
    {

        private readonly HttpClient _client;


        private readonly string BaseUrl = string.Empty;
        public SearchApiClient(HttpClient client, IConfiguration configuration)
        
        {
            _client = client;
             BaseUrl = configuration.GetSection("SearchApi").GetValue<string>("url");
        }

        public async Task<List<AdvertType>> Search(string keyword)
        {
            var result = new List < AdvertType > ();

            var url = $"{BaseUrl}/search/v1/{keyword}";
            var httpresponse = await _client.GetAsync(new Uri(url));

            if(httpresponse.StatusCode == HttpStatusCode.OK)
            {
                var allAdverts =  await httpresponse.Content.ReadAsAsync<List<AdvertType>>();
                result.AddRange(allAdverts);
            }
            return result;
        }
    }
}
