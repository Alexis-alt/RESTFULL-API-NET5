using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    //Clase abstracta de la cual heredan todas las entidades o models 
    public abstract class AuditableBaseEntity
    {

        public virtual int Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }


        public DateTime LastModified { get; set; }


    }
}
