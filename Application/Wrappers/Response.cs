using System.Collections.Generic;

namespace Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }


        //Cuando se ejecuta con exito
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        //Cuando no se lleva a cabo con exito
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        //Propiedades del objeto Respuesta
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T  Data { get; set; }
    }
}
