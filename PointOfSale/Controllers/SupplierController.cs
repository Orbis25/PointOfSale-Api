using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interface;

namespace PointOfSale.Controllers
{
    [Produces("application/json")]
    [Route("api/supplier")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _supplierService.GetAll());
        }

        [HttpGet("getbyid/{id}", Name = "GetByIdSupplier")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var model = await _supplierService.GetById(id);
            if (model != null) return Ok(model);
            return BadRequest("Este supplidor no existe");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Supplier model)
        {
            model.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (await _supplierService.Add(model))
                {
                    return new CreatedAtRouteResult(nameof(GetById), new { id = model.Id }, model);
                }
                return BadRequest("Lo sentimos, no se ha podido crear el suplidor");
            }
            return BadRequest("Algunos valores estan incorrectos");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Supplier model)
        {
            if (await _supplierService.Update(model)) return Ok(true);
            return BadRequest("Lo sentimos, no se ha podido actualozar el suplidor");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            if (await _supplierService.Delete(id)) return Ok(true);
            return BadRequest("Lo sentimos, lo sentimos no se ha podido eliminar el supplidor");
        }


    }
}