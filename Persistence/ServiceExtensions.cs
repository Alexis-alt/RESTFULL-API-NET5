using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repository;

namespace Persistence
{
    //Todos los servicios de extension se referencian en el Startup 


    public static class ServiceExtensions
    {

        public static void AddPersitenceInfraestructure(this IServiceCollection services,IConfiguration config)
        {


            //Obteniendo la cadena de conexión

            services.AddDbContext<ApplicationBbContext>(options => options.UseSqlServer(

                config.GetConnectionString("DeffaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationBbContext).Assembly.FullName)
                ));

            #region Repositories



            //Configuramos el IRepository para poder inyectarse

                                  //Indicamos que la clase Respository implementa la Interfaz IRepositoruAsync 
                                  //Ambas son genericas recibiendo una clase(Modelo/Entidad) como parmetro y adaptandole los métodos que implementa la interfaz 
            services.AddTransient(typeof(IRespositoryAsync<>),typeof(Repository<>));



            #endregion






        }



    }
}
