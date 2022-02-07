using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{

    //Clase que implementa el Repositorio
    //En realidad los hereda de la clase RepositoryBase la cual si contiene la definición de los métodos y que hereda de la interfaz IRepositoryBase
    //La creamos principalmente para indicarle el Contexto de BD que usará
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public MyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
