using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Web.Models.Home;
using WebAdvert.Web.ServiceApi;

namespace WebAdvert.Web.Controllers
{
    public class SearchApi : Controller
    {
        private readonly ISearchApiClient _client;
        private readonly IMapper _mapper;

        public SearchApi(ISearchApiClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;

        }
        
        public IActionResult Index()
        {
            var model = new searchViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task <IActionResult> Search(string keyword)
        {
            var viewModel = new List<searchViewModel>();
            var searchResult = await _client.Search(keyword);
          

            searchResult.ForEach(advertDoc =>
            {
              

                var searchresult = _mapper.Map<searchViewModel>(advertDoc);

                viewModel.Add(searchresult);
            }
            );
            return View("Search", viewModel);
        }

       
    }
}
