using Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Querys.GetAllClientes
{
    public class GetAllClientesParameters:RequestParameters
    {

        public string Nombre { get; set; }

        public string Apellidos { get; set; }


    }
}
