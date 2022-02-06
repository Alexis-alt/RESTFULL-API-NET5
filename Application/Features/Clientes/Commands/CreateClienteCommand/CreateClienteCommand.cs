using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Clientes.Commands.CreateClienteCommand
{


    //Clase que representa la data requerida en una request en este caso para un Command que permitira crear un cliente
    

    //El patron CQRS
    //Commands-Modificaciones en la base de datos
    //Querys-Consultas/Get


    //Tienen que heredar de IRequest<RespuestaDeseadaParaCliente>
    public class CreateClienteCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
    }





                                                               //Clase Command/Query //Respuesta que devolverá el proceso
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Response<int>>
    {

        //Inyectamos lo siguiente para poder hacer uso del patron IRepository para interacción con BD y para hacer el mapeo

                                        //Modelo con el cual se trabajará en esta clase
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;


        private readonly IMapper _mapper;


        //Constructor
        public CreateClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }



        //Este método se implementa de la interfaz IRequestHandler<Command/Query,Respuesta>

        //Logica de negocio que se ejecuta tras la llamada del controller
        public async Task<Response<int>> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {

                                        //Se mapean las propiedades de la solicitud al Model Cliente
            var nuevoRegistro = _mapper.Map<Cliente>(request);


            //Se hace la inserción 
            //Métodos de la interfaz IRepositoryAsync que hereda de la interfaz del paquete Ardalis que implementa estos métodos genericos
            var data = await _repositoryAsync.AddAsync(nuevoRegistro);



            //Retornamos una respuesta
            /*
            Response es una clase personalizada que instanciamos para devolver respuestas al cliente
            Seria practicamente lo que el controller recibiría
            Tiene 3 sobrecargas y recibe un parametro de tipo T cuyo valor representa el atributo Data 
            
            En este caso pasamos el id del nuevo registro de manera que colocamos un valor int para T
            
            
             
             */

            return new Response<int>(data.Id);
        }

    }
}
