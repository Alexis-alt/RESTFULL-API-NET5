using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class ApplicationBbContext : DbContext
    {


        private readonly IDateTimeService _dateTime;

        public ApplicationBbContext(DbContextOptions<ApplicationBbContext> options, IDateTimeService dateTime) :base(options)
        {


            //Buenas practicas
            //Optimiza el rendimiento de la aplicación ya que desactiva el seguimiento de cambios en las consultas o querys ahorrando recursos
            //Cuando las consultas llevan un SELECT el seguidor de cambios no se activa
            //Hay varias formas de configuracion, la siguientes es una 
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            _dateTime = dateTime;

        }



        public DbSet<Cliente> Clientes { get; set; }

        //EL método SaveChanges heredado de la clase DBContext es un método virtual, lo cual indica que clases que lo hererden pueden modificarlo (métodos con el mismo nombre y diferentes funcionalidades)
        //Métodos abstractos unicamnete indican que tienen que serimplementado en las subClases
        //Métodos con new ignoran la existencia de otro método parecido en la ClasePadre


        //Modificamos el método SaveChangesAsync para que cada vez que se guarde o edite un registro y se guarden los cambios, tambien se guarde la fecha
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {


                switch (entry.State)
                {

                    case EntityState.Added:

                        entry.Entity.Created = _dateTime.NowUtc;
                        
                        break;

                    case EntityState.Modified:

                        entry.Entity.LastModified = _dateTime.NowUtc;
                        
                        break;


                }



            }

      
            //Usamos el método original (El cual tiene 2 sobrecargas) proveniente de DbContext para guardar en BD 
            //Recordando que nos retorna un int (id del registro afectado)
            
            
            return base.SaveChangesAsync(cancellationToken);


        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

                                                          //El ensamblado detecta/scanea todas las clases que forman parte del proyecto  
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }



    }
}
