using Application.Wrappers;
using MediatR;
using System;

namespace Application.Features.Clientes.Commands.CreateClienteCommand
{
    public class CreateClienteCommand:IRequest<Response<int>>
    {


        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }




    }
}
