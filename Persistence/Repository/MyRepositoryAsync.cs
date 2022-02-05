using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationBbContext dbContext;

        public MyRepositoryAsync(ApplicationBbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
