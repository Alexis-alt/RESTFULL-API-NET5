using Application.DTOs.Users;
using Application.Features.Usuarios.Commands.DeleteUserCommand;
using Application.Features.Usuarios.Commands.UpdateUserCommand;
using Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {

        #region Commands

        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> DeleteUserAsync(DeleteUserCommand request);
        Task<Response<string>> UpdateUserAsync(UpdateUserCommand request);

        #endregion


        #region Querys

        public Task<Response<List<UsuarioDto>>> GetAllUsers();


        #endregion
    }
}
