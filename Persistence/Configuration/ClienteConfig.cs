using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{

    //Mapear las caracteristcas de los atributos en BD para hacer las migraciones
    class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {



        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //Mapea por el nombre de la tabla 

            builder.ToTable("Clientes");


           //Indicamos cual es la PK 
           //Entity siempre genra el identity cuando se hace codefirst
                           //El parametro que recibe esta lamda es el modelo (Recordemos que dicho modelo implemneta de una clase abtracta que le proporciona atributos generales)
                  builder.HasKey(p=>p.Id);

                 builder.Property(p => p.Nombre)
                .HasMaxLength(80)
                .IsRequired();

                builder.Property(p => p.Apellido)
               .HasMaxLength(80)
               .IsRequired();

               builder.Property(p => p.FechaNacimiento)
               .IsRequired();

               builder.Property(p => p.Telefono)
               .HasMaxLength(10);

               builder.Property(p => p.Email)
               .IsRequired()
               .HasMaxLength(10);


            builder.Property(p => p.Direccion)
            .IsRequired()
            .HasMaxLength(100);


            builder.Property(p=>p.Edad);



            builder.Property(p => p.CreatedBy)
            .IsRequired()
            .HasMaxLength(30);


            builder.Property(p =>p.LastModifiedBy )
            .IsRequired()
            .HasMaxLength(30);




        }
    }


}
