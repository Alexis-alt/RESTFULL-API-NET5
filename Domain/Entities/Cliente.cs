using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{


    public class Cliente:AuditableBaseEntity
    {

        private int _edad;


        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }

        //Regresar el número de años de una persona
        public int Edad
        {

            get
            {
                if(this._edad >= 0)
                {

                    this._edad = new DateTime(DateTime.Now.Subtract(this.FechaNacimiento).Ticks).Year - 1;

                }

                return this._edad;
            }

        }


    }
}
