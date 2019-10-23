using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Enums.Shared;
using Model.Enums.User;
using Model.Models;
using Model.ViewModels.User;
using Service.Interface;

namespace PointOfSale.Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmployeService _service;

        public AuthController(UserManager<User> userManager 
            , SignInManager<User> signInManager,
            IConfiguration configuration 
            , IEmployeService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _service = service;
        }

        [Route("create")]
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CreateUser([FromBody] UserVm model)
        {

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Email,
                    LastName = "none",
                    Rol = Model.Enums.User.Rol.user,
                    CreatedAt = DateTime.Now
                };


                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else
                {
                    return BadRequest("Intente nuevamente, no se ha podido crear el usuario");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserVm userInfo)
       {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return BuildToken(userInfo);
                }
                else
                {
                    return BadRequest(new { messageError = "Ha ocurrido un error porfavor revise sus datos" });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken(UserVm model)
        {
            var claims = new[]
            {
                //utilizamos los claims para enviar algunas informaciones
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            //creanos una llave secreta y la configuramos como variable de entorno
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret_Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "devteams.com",
               audience: "devteams.com",
               claims: claims,
               signingCredentials: creds);

            var userDetail = _service.GetbyEmail(model.Email);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                user = model.Email,
                id = userDetail != null ? userDetail.Id.ToString() : string.Empty,
                userName = userDetail == null ? string.Empty : userDetail.User.UserName,
                userData = userDetail != null ? new { state = userDetail.State, role = userDetail.User.Rol } : new { state = State.active, role = Rol.admin }
            });

        }
    }
}