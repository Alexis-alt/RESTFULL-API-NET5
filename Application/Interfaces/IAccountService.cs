using Application.DTOs.Users;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        //Métodos para gestionar los usuarios

        Task<Response<AuthenticationResponse>> AuthenticaAsync(AuthenticationRequest request, string ipAddress);

        Task<Response<string>> ResgisterAsync(RegisterRequest request, string origin);

    }
}
