using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Middleware;

namespace WebAPI.Extensions
{
    public static class AppExtensions
    {



        
        public static void UseErrorHandleMiddleware(this IApplicationBuilder app)
        {


            //Añadiendo el middleware de validación de peticiones 
            app.UseMiddleware<ErrorHandlerMiddleware>();

        }


    }
}
