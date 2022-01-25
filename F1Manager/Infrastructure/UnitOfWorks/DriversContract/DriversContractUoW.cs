using Domain.Contracts;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.DriversContract
{
    public class DriversContractUoW : IDriversContractUnitOfWork
    {
        private readonly AppDbContext context;

        public DriversContractUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            DriversContract = new ContractRepository(dbContext, loggerFactory);
        }

        public IContractRepository DriversContract { get; set; }

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