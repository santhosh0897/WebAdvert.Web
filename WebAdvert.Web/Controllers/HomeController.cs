﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.Home;

using WebAdvert.Web.ServiceApi;

namespace WebAdvert.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ISearchApiClient _client;
        private readonly IMapper _mapper;

        public HomeController(ISearchApiClient client, IMapper mapper, ILogger<HomeController> logger)
        {
            _client = client;
            _mapper = mapper;
            _logger = logger;

        }

       

    [Authorize]
        public IActionResult Index()
        {
            var model = new searchViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string keyword)
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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
