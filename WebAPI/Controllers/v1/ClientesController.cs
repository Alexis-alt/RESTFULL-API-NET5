using Application.Features.Clientes.Commands.DeleteClienteCommand;
using Application.Features.Clientes.Commands.UpdateClienteCommand;
using Application.Features.Clientes.Querys.GetAllClientes;
using Application.Features.Clientes.Querys.GetClienteByIdQuery;
using Application.Feautres.Clientes.Commands.CreateClienteCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientesController : BaseApiController
    {


        //GET All con paginación y filtros por Nombre o Apellidos
        [HttpGet]



                                                        //GetAllClientesParametres es una Clase que guarda todos los filtros para la busqueda(Nombre y Apellido) 
                                                        //Hereda de Requestparameters donde se encuentran parametros genrales (NumberPage y PageSize)´para la paginación
        public async Task<IActionResult> Get([FromQuery] GetAllClientesParameters filter)
        {

            //Se envia la request al Handler
            return Ok(await Mediator.Send(new GetAllClientesQuery { PageNumber= filter.PageNumber,PageSize=filter.PageSize,Nombre = filter.Nombre,Apellidos=filter.Apellidos }));

        }



        //GET api/<controller> By Id

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {

            return Ok(await Mediator.Send(new GetClienteByIdQuery {Id= id }));
        
        }



        //POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        //PUT api/controller

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, UpdateClienteCommand command)
        {

            if (id != command.Id)
                return BadRequest();


            return Ok(await Mediator.Send(command));
        }




        //DELETE    
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {


            return Ok(await Mediator.Send(new DeleteClienteCommand() { Id = id }));
        }



    }
}
