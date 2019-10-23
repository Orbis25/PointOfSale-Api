using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels.Product;
using Service.Interface;

namespace PointOfSale.Controllers
{
    [Produces("application/json")]
    [Route("api/product")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IImageWriter _imageWriter;
        public ProductController(IProductService productService , IImageWriter imageWriter)
        {
            _productService = productService;
            _imageWriter = imageWriter;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet("getbyid/{id}", Name = "GetByIdProduct")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var model = await _productService.GetById(id);
            if (model != null) return Ok(model);
            return BadRequest("Este Producto no existe");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Product model)
        {
            model.CreateAt = DateTime.Now; 
            if(ModelState.IsValid)
            {
                if (await _productService.Add(model))
                {
                    return new CreatedAtRouteResult(nameof(GetById), new { id = model.Id }, model);
                }
            }
            return BadRequest("Lo sentimos no se ha podido agregar el producto");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Product model)
        {
            if (await _productService.Update(model)) return Ok(true);
            return BadRequest("Lo sentimos no se ha podido actualizar el producto");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (await _productService.Delete(id)) return Ok(true);
            return BadRequest("Lo sentimos no se ha podido eliminar el producto");
        }

        [HttpPost("upload/avatar")]
        public async Task<IActionResult> UploadImage(ProductImageVM model)
        {
            string avatar = await _imageWriter.UploadImageAsync(model.File);
            if (await _productService.UpdateAvatar(new Product() { Id = model.Id, Avatar = avatar }))
            {
                return Ok(true);
            }
            return BadRequest("Lo sentimos , no se ha podido agregar la imagen");
        }

        [HttpGet("spents")]
        public async Task<IActionResult> ProductsSpents()
        {
            return Ok(await _productService.ProductsSpent());
        }

    }
}