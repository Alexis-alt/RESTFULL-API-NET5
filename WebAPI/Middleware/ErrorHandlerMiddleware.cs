using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    //Personalizando una respuesta Http para cuando se prosuce un error en el Middleware

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;


        //Recibimos la solicitud del cliente
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }




        //Método que se ejecuta en el Middleware

        public async Task Invoke(HttpContext context)
        {

            //Si no se encuentran problemas
            try
            {
                //Sique al siguiente filtro/pipe que se aplica en el Middleware
                //En este caso ejecuta el método de la clase VlidationBehaviour el cual se encarga de verificar validacioes en la request del cliente

                await _next(context);
            }
            catch (Exception error)
            {

                //Cuando se encuentra una exception
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

                switch (error)
                {
                    case Application.Exceptions.ApiException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case Application.Exceptions.ValidationException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        //not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
