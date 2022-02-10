using Application.Features.Clientes.Commands.DeleteClienteCommand;
using Application.Features.Clientes.Commands.UpdateClienteCommand;
using Application.Features.Clientes.Querys.GetClienteByIdQuery;
using Application.Feautres.Clientes.Commands.CreateClienteCommand;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientesController : BaseApiController
    {

        //GET api/<controller> By Id

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {

            return Ok(await Mediator.Send(new GetClienteByIdQuery {Id= id }));
        
        }



        //POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        //PUT api/controller

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, UpdateClienteCommand command)
        {

            if (id != command.Id)
                return BadRequest();


            return Ok(await Mediator.Send(command));
        }




        //DELETE    
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {


            return Ok(await Mediator.Send(new DeleteClienteCommand() { Id = id }));
        }



    }
}
