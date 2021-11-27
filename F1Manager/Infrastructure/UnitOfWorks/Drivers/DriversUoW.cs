using Domain.Drivers;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Drivers
{
    public class DriversUoW : IDriversUnitOfWork
    {
        private readonly AppDbContext context;

        public DriversUoW(AppDbContext dbContext)
        {
            this.context = dbContext;
            Drivers = new DriverRepository(dbContext);
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