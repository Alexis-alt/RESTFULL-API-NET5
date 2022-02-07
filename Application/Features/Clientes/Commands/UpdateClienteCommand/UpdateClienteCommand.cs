using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateClienteCommand: IRequest<Response<int>>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }


    }


    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Response<int>>
    {

        private readonly IRepositoryAsync<Cliente> _repositoryAsync;


        private readonly IMapper _mapper;


        public UpdateClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {

            _repositoryAsync = repositoryAsync;
            _mapper = mapper;


        }





        public async Task<Response<int>> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {


            var recordToModif = await _repositoryAsync.GetByIdAsync(request.Id);

            if(recordToModif == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el Id: {request.Id}");
            }
            else
            {

                recordToModif.Nombre = request.Nombre;
                recordToModif.Apellido = request.Apellido;
                recordToModif.FechaNacimiento = request.FechaNacimiento;
                recordToModif.Telefono = request.Telefono;
                recordToModif.Email = request.Email;
                recordToModif.Direccion = request.Direccion;

                await _repositoryAsync.UpdateAsync(recordToModif);



                return new Response<int>(recordToModif.Id,"Se modificó correctamente");

            }




        }
    }



}
