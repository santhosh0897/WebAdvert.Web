using AdvertApi.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAdvert.Web.ServiceApi
{
    public class AdvertApiClient : IAdvertApiClient

    {
        private readonly string createurl;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        public AdvertApiClient(IConfiguration configuration, HttpClient client, IMapper mapper)
             
        {
            _configuration = configuration;
            _client = client;
            _mapper = mapper;

             createurl = _configuration.GetSection("AdvertApi").GetValue<string>("BaseUrl");

           // _client.BaseAddress = new Uri(createurl);

            //_client.DefaultRequestHeaders.Add("Content-type", "application/json");

               
        }


        public async Task<CreateAdvertResponse> Create(CreateAdvertModel models)
        {
            var advertApiModel = _mapper.Map<AdvertModels>(models);

            var JsonModel = JsonConvert.SerializeObject(advertApiModel);

            //var response = await _client.PostAsync(new Uri("{_client.BaseAddress}/create"), new StringContent(JsonModel));

            var response = await _client.PostAsync(new Uri($"{createurl}/create"),
                new StringContent(JsonModel, Encoding.UTF8, "application/json"));

            var responseJson = await response.Content.ReadAsStringAsync();

            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);

            var advertresponse = _mapper.Map<CreateAdvertResponse>(createAdvertResponse);

            return advertresponse;
        }


        public async Task<bool> Confirm(ConfirmAdvertRequest confirmAdvertRequest)
        {
            var advertModel = _mapper.Map<ConfirmAdvertModel>(confirmAdvertRequest);

            var JsonModel = JsonConvert.SerializeObject(advertModel);

            var response = await _client.PutAsync(new Uri($"{createurl}/confirm"),
                new StringContent(JsonModel, Encoding.UTF8, "application/json"));

           // var response = await _client.PutAsync(new Uri("{_client.BaseAddress}/confirm"), new StringContent(JsonModel));

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
