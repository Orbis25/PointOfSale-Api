using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Enums.User;
using Model.Models;
using Model.ModelsMappings;
using Model.ViewModels.Employee;
using Model.ViewModels.User;
using Service.Interface;

namespace PointOfSale.Controllers
{
    [Produces("application/json")]
    [Route("api/employee")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmployeService _employeeService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IImageHandlerService<IActionResult> _image;
        public EmployeeController(IEmployeService employeService 
            , UserManager<User> userManager ,
             IUserService userService,
             IMapper mapper,
              IImageHandlerService<IActionResult> image)
        {
            _employeeService = employeService;
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
            _image = image;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<EmployeeDTO>>(await _employeeService.GetAll()));
        }

        [HttpGet("getbyid/{id}", Name = "GetByIdEmployee")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var model = _mapper.Map<EmployeeDTO>(await _employeeService.GetById(id));
            if (model != null)
            {
                return Ok(model);
            }
            return BadRequest("Usuario no encontrado");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserEmployeeVm model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name = model.Name,
                    LastName = model.LastName,
                    Rol = Rol.employee,
                    CreatedAt = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var profile = await _userService.GetUserByEmail(model.Email);
                    var employee = new Employee
                    {
                        Address = model.Address,
                        Avatar = "ninguno",
                        Dni = model.Dni,
                        EmployeeCode = model.EmployeeCode,
                        PhoneNumber = model.PhoneNumber,
                        State = Model.Enums.Shared.State.active,
                        UserId = profile.Id
                    };

                    if (await _employeeService.Add(employee))
                    {
                        return new CreatedAtRouteResult(nameof(GetById), new { id = employee.Id }, model);
                    }
                }
            }
            return BadRequest("Ha ocurrido un error no se ha creado el empleado");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserEmployeeVm model)
        {
            if (await _employeeService.Update(_mapper.Map<Employee>(model))) return Ok(true);
            return BadRequest("El empleado no ha podido ser actualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (await _employeeService.Delete(id)) return Ok(true);
            return BadRequest("No se ha podido eliminar el empleado");
        }

        [HttpPost("upload/img")]
        public async Task<IActionResult> UploadImage(ImgUpload model)
        {
            dynamic imgName = await _image.UploadImage(model.File);
            return Ok(await _employeeService.UpdateImg(new Employee { Id = model.Id, Avatar = imgName.Value }));
        }

        [HttpPost("changes/password")]
        public async Task<IActionResult> ChangePassword([FromBody] EmployeChangePasswordVm model)
        {
            if (ModelState.IsValid)
            {
                var user = await _employeeService.GetById(model.Id);
                user.User.PasswordHash = _userManager.PasswordHasher.HashPassword(user.User, model.Password);
                var result = await _userManager.UpdateAsync(user.User);
                if (result.Succeeded) return Ok(true);
            }
            return BadRequest(model);
        }
    }
}