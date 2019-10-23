using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interface;
using System;
using System.Threading.Tasks;

namespace PointOfSale.Controllers
{
    [Produces("application/json")]
    [Route("api/client")]
    [Authorize(AuthenticationSchemes  = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _clientService.GetAll());
        }

        [HttpGet("getbyid/{id}", Name = "GetById")]
        public async Task<IActionResult> Getbyid([FromRoute] Guid id)
        {
            var model = await _clientService.GetById(id);
            if (model != null) return Ok(model);
            return BadRequest("Este cliente no existe");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Client model)
        {
            model.State = Model.Enums.Shared.State.active;
            model.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            if (ModelState.IsValid)
            {   
                if (await _clientService.Add(model))
                {
                    return new CreatedAtRouteResult(nameof(Getbyid), new { id = model.Id }, model);
                }
            }
            else
            {
                return BadRequest("Algunos datos proporcionados son incorrectos");
            }

            return BadRequest("Ha ocurrido un error al crear el cliente");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Client model)
        {
            if (await _clientService.GetById(model.Id) != null)
            {
                return Ok(_clientService.Update(model));
            }

            return BadRequest("Ha ocurrido un error el cliente no ha sido actualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            return Ok(await _clientService.Delete(id));
        }


    }
}