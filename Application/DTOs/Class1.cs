using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
   
    public record ClienteDto(int Id,string Nombre, string Apellido, DateTime FechaNacimiento,string Telefono,string Email,string Direccion, int Edad);




}
