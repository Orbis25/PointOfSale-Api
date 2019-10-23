using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace PointOfSale.Controllers
{
    [Produces("application/json")]
    [Route("api/Home")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly IReportService _service;
        public HomeController(IReportService service) => _service = service;

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.CountAll());
    }
}