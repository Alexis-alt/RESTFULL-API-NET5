using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using Shared;
using WebAPI.Extensions;

namespace WebAPI
{
    public class Startup
    {




        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public IConfiguration Configuration { get; }





        // This method gets called by the runtime. Use this method to add services to the container.
        //Servicio: Modulo reusable que provee funcionalidades (Paquetes nuget o configuración para inyectar)
        public void ConfigureServices(IServiceCollection services)
        {





            //Vienen de Core de la Aplicacion
            //Referencia a los servicios necesarios para el funcionamiento
            //Startup necesita de el asi que tiene que matricularlo
            services.AddApplicationLayer();

            services.AddSharedInfraestructure(Configuration);

            services.AddPersitenceInfraestructure(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }





        //Middleware
        //Filtros o tuberías que se agregan antes de que la solicitud llegue al servidor o una vez que ha salido de el como una respuesta
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();





            //Pipe construido para validar solicitudes antes de que lleguen al servidor
            app.UseErrorHandleMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
