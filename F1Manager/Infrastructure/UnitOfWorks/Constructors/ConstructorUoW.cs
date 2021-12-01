using Domain.Constructors;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Constructors
{
    public class ConstructorUoW : ICounstructorUnitOfWork
    {
        private readonly AppDbContext context;

        public ConstructorUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            Constructors = new ConstructorRepository(dbContext,loggerFactory);
        }

        public IConstructorRepository Constructors { set; get; }

        public Task<int> Commit()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}