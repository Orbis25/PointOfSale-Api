using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ModelsMappings;
using Model.ViewModels.Sale;
using Newtonsoft.Json;
using Service.Interface;

namespace PointOfSale.Controllers
{
    [Produces("application/json")]
    [Route("api/sale")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SaleController : Controller
    {
        private readonly ISaleService _service;
        private readonly IMapper _mapper;
        public SaleController(ISaleService saleService,
            IMapper mapper)
        {
            _service = saleService;
            _mapper = mapper;

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("getbyid/{id}", Name = "GetByIdSale")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var model = _mapper.Map<SaleDto>(await _service.GetById(id));
            return Ok(new
            {
                sale = model ,
                products = await _service.GetsProductBySaleId(model.Id)
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SaleVM model)
        {
            if (ModelState.IsValid)
            {
                if (await _service.AddSV(model))
                {
                    return new CreatedAtRouteResult(nameof(GetById), new { id = model.Id }, model);
                }
            }  
            return BadRequest("No se ha podido hacer la venta");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (await _service.Delete(id)) return Ok(true);
            return BadRequest("Lo sentimos no se ha podido eliminar la venta");
        }
    }
}