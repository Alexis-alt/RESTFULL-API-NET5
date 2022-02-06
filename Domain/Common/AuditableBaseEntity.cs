using System;

namespace Domain.Common
{


    //Clase de la cual heredan todas las Entidades o Modelos, debido a que son propiedades que se tienen en común
    public abstract class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
