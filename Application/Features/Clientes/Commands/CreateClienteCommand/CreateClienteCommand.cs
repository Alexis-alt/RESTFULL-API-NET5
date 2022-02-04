using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
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

        //Referencia al modelo o entidad
        private readonly IRespositoryAsync<Cliente> _respositoryAsync;

        private readonly IMapper _mapper;

   

        public CreateClienteCommandHandler(IRespositoryAsync<Cliente> respositoryAsync,IMapper mapper)
        {
            _respositoryAsync = respositoryAsync;

            _mapper = mapper;
        }





        //Este método se implementa de la interfaz IRequestHandler<TRequest,TResponse>
        //Implemeta toda la logica de negocio

        //Este método devuelve una objeto del tipo response
        /*
         Dicho objeto que se devulve contiene
            -Exito bool
            -Mensaje string
            -Lista de errores string
            -Data <T> generico que en este caso indicamos que será un int debido a que como vamos a crear un rescurso obtendremos el id del recurso creado

         
         */
        public async Task<Response<int>> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {

                                        //Mapeame a 
                                                    //Lo que venga en la solicitud o request
            var nuevoRegistro = _mapper.Map<Cliente>(request);
                                              //Añade el registro
            var data = await _respositoryAsync.AddAsync(nuevoRegistro);


            //Mandamos a una sobrecarga de ctor el id 
            //Debe ser int porque al instanciar la Clase indicamos que el T debe ser int

            //Se retorna una respuesta 
            return new Response<int>(data.Id);

        }
    }



}
