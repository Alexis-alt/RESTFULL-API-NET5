
using Application.DTOs;
using Application.Feautres.Clientes.Commands.CreateClienteCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            //Damos de alta o matriculamos que clase se tiene que mapear con cual


            #region DTOs

            CreateMap<Cliente, ClienteDto>();


            #endregion


            #region Commands
            CreateMap<CreateClienteCommand, Cliente>();
            #endregion
        }
    }
}
