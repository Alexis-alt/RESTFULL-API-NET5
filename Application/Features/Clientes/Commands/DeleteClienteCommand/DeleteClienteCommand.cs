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

namespace Application.Features.Clientes.Commands.DeleteClienteCommand
{


    //Command to Delete
   public class DeleteClienteCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }



    }






    //Mediator or Handler
    public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, Response<int>>
    {


        private readonly IRepositoryAsync<Cliente> _repositoryAsync;


        private readonly IMapper _mapper;

        public DeleteClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }




        public async Task<Response<int>> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {

            var recordToDelete = await _repositoryAsync.GetByIdAsync(request.Id);

            if (recordToDelete == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el Id: {request.Id}");
            }
            else
            {


             

                await _repositoryAsync.DeleteAsync(recordToDelete);



                return new Response<int>(recordToDelete.Id,"Se eliminó correctamente");

            }



        }
    }




}
