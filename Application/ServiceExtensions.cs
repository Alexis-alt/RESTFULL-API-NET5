using Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    //Agrupa las inyecciones de todos los servicios propios o de terceros que se contengan en este proyecto para despues referenciarlos en startup y este pueda usarlos

    public static class ServiceExtensions
    {
        //Este tipo de configuraciones se hacen generalemente en la clase startup pero como realizamos un desacomplamiento lo configuramos de esta manera
        //Para ello se han instalado los paquetes nuguet necesarios Y su dependency inyection para poder ser usados
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }


    }
}
