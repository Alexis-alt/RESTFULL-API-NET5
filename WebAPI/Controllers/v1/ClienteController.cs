using Application.Feautres.Clientes.Commands.CreateClienteCommand;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{


    [ApiVersion("1.0")]

    public class ClientesController : BaseApiController
    {

        //Toda la logica en el Core

        //POST api/Controller

        [HttpPost]

        public async Task<IActionResult> Post(CreateClienteCommand command)
        {

            return Ok(await Mediator.Send(command));
        }



    }
}
