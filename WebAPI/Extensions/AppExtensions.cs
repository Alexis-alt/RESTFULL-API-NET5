using Microsoft.AspNetCore.Builder;
using WebAPI.Middlewares;

namespace WebAPI.Extensions
{
    public static class AppExtensions
    {

        //Middlewares que se agregan a startup
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
