using Application.DTOs.Users;
using Application.Features.Usuarios.Commands.AuthenticateCommand;
using Application.Features.Usuarios.Commands.DeleteUserCommand;
using Application.Features.Usuarios.Commands.RegisterCommand;
using Application.Features.Usuarios.Commands.UpdateUserCommand;
using Application.Features.Usuarios.Querys.GetAllUsuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateCommand { 
                Email = request.Email,
                Password = request.Password,
                IpAddress = GenerateIPAddress()
            }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterCommand
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                //Header de la solicitud
                Origin = Request.Headers["origin"]
            }));
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }


        [HttpDelete("delete")]

        public async Task<IActionResult> DeleteAsync(string idUser)
        {

            return Ok(await Mediator.Send(new DeleteUserCommand { 
            
                IdUser = idUser
            
            }));
        }


        [HttpPut("update")]

        public async Task<IActionResult> UpdateAsync(string id, UpdateUserCommand command)
        {
            if (id != command.IdUser)
                return BadRequest();

            return Ok(await Mediator.Send(command)); ;
        }



        [HttpGet("getAll")]

        public async Task<IActionResult> GetAllUsers()
        {


            return Ok(await Mediator.Send(new GetAllUsuariosQuery()));
        }





    }
}
