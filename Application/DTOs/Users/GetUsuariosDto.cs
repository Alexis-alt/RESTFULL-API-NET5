using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{

    public record UsuarioDto(string IdUser, string Nombre, string Apellido,string UserName,string Password,string Email, string RoleId, string RoleName);




}
