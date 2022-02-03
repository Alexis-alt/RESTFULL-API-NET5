using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    //Clase que es modelo para regresar una respuesta, la cual contiene un Success, Mensaje y Data

    public class Response<T>
    {

        public Response()
        {

        }


        //Constructor cuando se procesa sin errores 
        public Response(T data, string message = null)
        {

            Success = true;

            Message = message;

            Data = data;



        }



        //Constructor que se usa para indicar un error 
        public Response(string message)
        {

            Success = false;

            Message = message;

        }


        public bool Success { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T Data { get; set; }


    }
}
