using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Commands.UpdateUserCommand
{
    public class UpdateUserCommand: IRequest<Response<string>>
    {
        public string IdUser { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        

    }


    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<string>>
    {

        private readonly IAccountService _accountService;

        public UpdateUserCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            return await _accountService.UpdateUserAsync(new UpdateUserCommand
            {

                IdUser = request.IdUser,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                UserName = request.UserName,
                Email = request.Email

            }); 

        }
    }



}
