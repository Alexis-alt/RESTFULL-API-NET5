using Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Commands.CreateClienteCommand
{

    //Se esta implementando el patron Mediator

    public class CreateClienteCommand:IRequest<Response<int>>
    {


        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }


    }

    //CLASE MEDIADORA O MANEJADORA
    //El controller llamará a esta clase manejadora o mediadora

    //Recibe un obj que implemente de la inetrfaz IRquest<ClaseDeRespuesta>
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Response<int>>
    {


        //Este método se implementa de la interfaz IRequestHandler<TRequest,TResponse>
        

        //Este método devuelve una objeto del tipo response
        /*
         Dicho objeto que se devulve contiene
            -Exito bool
            -Mensaje string
            -Lista de errores string
            -Data <T> generico que en este caso indicamos que será un int debido a que como vamos a crear un rescurso obtendremos el id del recurso creado

         
         */
        public Task<Response<int>> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }



}
