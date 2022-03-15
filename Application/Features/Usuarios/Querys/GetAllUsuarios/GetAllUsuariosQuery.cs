using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Querys.GetAllUsuarios
{
   public class GetAllUsuariosQuery: IRequest<Response<List<UsuarioDto>>>
    {
        public string IdUser { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }




        public class GetAllUsuariosQueryHandler : IRequestHandler<GetAllUsuariosQuery, Response<List<UsuarioDto>>>
        {


            private readonly IAccountService _accountService;

            public GetAllUsuariosQueryHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public  async Task<Response<List<UsuarioDto>>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
            {

   

                return await _accountService.GetAllUsers();

              

            }






        }




    }





}
