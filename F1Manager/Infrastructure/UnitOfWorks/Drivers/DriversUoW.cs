using Domain.Drivers;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Drivers
{
    public class DriversUoW : IDriversUnitOfWork
    {
        private readonly AppDbContext context;

        public DriversUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            Drivers = new DriverRepository(dbContext, loggerFactory);
        }

        public IDriverRepository Drivers { get; set; }

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