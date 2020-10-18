using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.Service;
using WebAdvert.Web.ServiceApi;

namespace WebAdvert.Web.Controllers
{
    public class AdvertManagement : Controller
    {
        private readonly IFileUploader _fileUploader;
        private readonly IAdvertApiClient _adverapi;
        private readonly IMapper _mapper;
        bool isOkToConfirmAd = true;

        public AdvertManagement(IFileUploader fileUploader, IAdvertApiClient adverapi, IMapper mapper)
        {
            _fileUploader = fileUploader;
            _adverapi = adverapi;
            _mapper = mapper;
        }

        public IActionResult Create()
        {

            var model = new CreateAdvertViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task <IActionResult> Create(CreateAdvertViewModel model, IFormFile imagefile)
        {

            if (ModelState.IsValid)
            {
                //var id = "11111";
                //Call advert Api to store details in DB

                var createadvertmodel = _mapper.Map<CreateAdvertModel>(model);

                var apiCallresponse = await _adverapi.Create(createadvertmodel);

                createadvertmodel.UserName = User.Identity.Name;

                var id = apiCallresponse.Id;
               var filepath = string.Empty;

                if (imagefile != null)
                {
                   var fileName = !string.IsNullOrEmpty(imagefile.FileName) ? Path.GetFileName(imagefile.FileName) : id;
                     filepath = $"{id }/{fileName}";

                    try
                    {
                        using (var readstream = imagefile.OpenReadStream())
                        {
                            var result = await _fileUploader.UploadFileAsync(filepath, readstream);

                            if (!result)
                            {
                                throw new Exception
                                (
                                    "could not upload image to file directory. Please see the logs for details"
                                );
                            }

                          
                        }

                    }
                    catch(Exception e)
                    {
                        //rollback
                        isOkToConfirmAd = false;
                        var confirmmodel = new ConfirmAdvertRequest
                        {
                            Id = id,
                            FilePath = filepath,
                            Status = AdvertStatus.pending
                        };

                       await _adverapi.Confirm(confirmmodel);


                        Console.WriteLine(e);
                    }
                


                }

                if (isOkToConfirmAd)
                {
                    var confirmModel = new ConfirmAdvertRequest()
                    {
                        Id = id,
                        FilePath = filepath,
                        Status = AdvertStatus.Active
                    };
                    await _adverapi.Confirm(confirmModel);
                }

                return RedirectToAction("Index", "Home");


            }
            return View(model);
        }
    }
}
