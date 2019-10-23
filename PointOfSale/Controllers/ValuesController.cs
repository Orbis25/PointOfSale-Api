using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Rest.Azure;
using Service.Interface;

namespace PointOfSale.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        private readonly IImageHandlerService<IActionResult> _image;
        public ValuesController(IImageHandlerService<IActionResult> image)
        {
            _image = image;
        }

        //test image
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return await _image.UploadImage(file);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Hola = "" });
        }



    }
}
