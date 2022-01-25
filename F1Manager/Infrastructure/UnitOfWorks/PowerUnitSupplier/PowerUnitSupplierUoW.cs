using Domain.PoweUnitSupplier;
using Infrastructure.DataAccess;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.PowerUnitSupplier
{
    public class PowerUnitSupplierUoW : IPowerUnitSupplierUnitOfWork
    {
        private readonly AppDbContext context;

        public PowerUnitSupplierUoW(AppDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            PowerUnitSupplier =
                new Infrastructure.DataAccess.Repositores.PowerUnitSupplierRepository(this.context, loggerFactory);
        }

        public IPowerUnitSupplier PowerUnitSupplier { get; set; }

        public Task<int> Commit()
        {
            return context.SaveChangesAsync();
        }
    }
}