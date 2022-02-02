using Ardalis.Specification.EntityFrameworkCore;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Contexts;

namespace Persistence.Repository
{

    //Clase que implmenta el Repository

                                 //Clase que proviene del nuget y contiene los métodos CRUD genericos
    public class Repository<T> : RepositoryBase<T>, IRespositoryAsync<T> where T : class
    {

        private readonly ApplicationBbContext _dbContext;

        public Repository(ApplicationBbContext dbContext):base(dbContext)
        {


            _dbContext = dbContext;

        }
    }
}
