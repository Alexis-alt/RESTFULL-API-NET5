using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    //Personaliza las respuestas de Error dadas por http

    public class ErrorHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //La clase HttpContext almacena toda la informacion de la solicitud HTTP el contexto de la solicitud actual del dominio de aplicación.
        //Intercepta las solicitudes Http
        public async Task Invoke(HttpContext context)
        {

            try
            {

                await _next(context);
                     



            }
            catch (Exception e)
            {


                var response = context.Response;

                response.ContentType = "application/json";

                var responseModel = new Response<string>() {

                    Success = false,
                    Message = e?.Message,

                
                };



                //Encargado de definir el código de Estado de la respuesta cuando se produce un error
                switch (e)
                {
                    
                    //Exceptions de validación
                    case Application.Exceptions.ValidationException ex:

                        //De tipo int 
                        
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = ex.Errors;

                        break;
                        //Error de Api
                    case Application.Exceptions.ApiException ex:

                        response.StatusCode = (int)HttpStatusCode.BadRequest;



                        break;
                        //Error de id recurso no encontrado (id)
                    case KeyNotFoundException ex:

                        response.StatusCode = (int)HttpStatusCode.NotFound;



                        break;

                    default:
                        //Cuando es un tipo de error no manejado es decir diferente a los anteriores


                        response.StatusCode = (int)HttpStatusCode.InternalServerError;



                        break;




                }

               //JSON string
                var result = JsonSerializer.Serialize(responseModel);

                //Escribe el texto mandado por parametro en el cuerpo de la respuesta
                await response.WriteAsync(result);


            }



        }


    }
}
