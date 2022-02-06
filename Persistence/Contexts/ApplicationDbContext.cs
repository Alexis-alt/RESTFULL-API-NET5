using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime) : base (options)
        {


            //Buena practica: optimización de querys
            //Desactivando el seguidor de cambios para las querys puesto que solo se estan consultando datos y no modificando por tanto no requieren un seguimiento
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }

        //Tables
        public DbSet<Cliente> Clientes { get; set; }



        //EL método SaveChanges heredado de la clase DBContext es un método virtual, lo cual indica que clases que lo hererden pueden modificarlo (métodos con el mismo nombre y diferentes funcionalidades)
        //Métodos abstractos unicamnete indican que tienen que serimplementado en las subClases
        //Métodos con new ignoran la existencia de otro método parecido en la ClasePadre


        //Modificamos el método SaveChangesAsync para que cada vez que se guarde o edite un registro y se guarden los cambios, tambien se guarde la fecha


        public override Task<int> SaveChangesAsync(CancellationToken cancellationTaken = new CancellationToken())
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



            //Guarda los datos y regresa un int con el id del registro que se afectó

            //Usamos el método original (El cual tiene 2 sobrecargas) proveniente de DbContext para guardar en BD 
            //Recordando que nos retorna un int (id del registro afectado)

            return base.SaveChangesAsync(cancellationTaken);
        }




        //Este método se ejecuta cada vez que se hace una nueva migración
        //Mapea las configuraciones de los Models para aplicarlos en BD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

                                                          //El ensamblado detecta/scanea todas las clases que forman parte del proyecto  
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
