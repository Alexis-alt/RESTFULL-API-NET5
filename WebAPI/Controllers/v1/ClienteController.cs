using Application.Features.Clientes.Commands.CreateClienteCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{


    [ApiVersion("1.0")]

    public class ClienteController : BaseApiController
    {

        //Toda la logica en el Core

        //POST api/Controller

        [HttpPost]

        public async Task<IActionResult> Post(CreateClienteCommand command)
        {




            return Ok(Mediator.Send(command));
        }



    }
}
