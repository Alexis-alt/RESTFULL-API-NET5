using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    class PagedClientesSpecifications:Specification<Cliente>
    {

        //Revisar esto
        //Clase creada para manejar especificaciones en las busquedas


        public PagedClientesSpecifications(int pageSize, int pageNumber, string nombre, string apellido)
        {

            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            //Es como hacer un like en SQL 
            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Nombre, "%" + nombre + "%");


            if (!string.IsNullOrEmpty(apellido))
                Query.Search(x => x.Apellido, "%"+apellido+"%");

        }
    }
}
