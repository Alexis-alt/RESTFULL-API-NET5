using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Commands.DeleteUserCommand
{
    public class DeleteUserCommand:IRequest<Response<string>>
    {

        public string IdUser { get; set; }

    }



    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<string>>
    {

        private readonly IAccountService _accountService;

        public DeleteUserCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            return await _accountService.DeleteUserAsync(new DeleteUserCommand { 
            
            IdUser = request.IdUser

            });

        }
    }


}
