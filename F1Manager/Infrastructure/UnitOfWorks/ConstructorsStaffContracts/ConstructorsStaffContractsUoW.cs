using Domain.ConstructorsStaffContracts;
using Infrastructure.DataAccess;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.ConstructorsStaffContracts
{
    public class ConstructorsStaffContractsUoW : IConstructorsStaffContractsUnitOfWork
    {
        private readonly AppDbContext context;

        public ConstructorsStaffContractsUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            ConstructorsStaffContracts =
                new Infrastructure.DataAccess.Repositores.ConstructorStaffContractRepository(dbContext, loggerFactory);
        }

        public IConstructorsStaffContractsRepository ConstructorsStaffContracts { set; get; }

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