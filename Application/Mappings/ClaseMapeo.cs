using Application.Features.Clientes.Commands.CreateClienteCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ClaseMapeo:Profile
    {


        public ClaseMapeo()
        {

            #region Comandos 

            //Matriculado los mapeos
            //Se colocan las clases que se van a mapear
            CreateMap<CreateClienteCommand, Cliente>();








            #endregion






        }



    }
}
