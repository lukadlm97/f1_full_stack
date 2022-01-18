using Domain.TechnicalStaff;
using Infrastructure.DataAccess;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.TechnicalStuff
{
    public class TechnicalStaffUoW : ITechnicalStaffUnitOfWork
    {
        private readonly AppDbContext context;

        public TechnicalStaffUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            TechnicalStaffRepository = new Infrastructure.DataAccess.Repositores.TechnicalStaffRepository(dbContext, loggerFactory);
        }

        public ITechnicalStaffRepository TechnicalStaffRepository { get; set; }

        public async Task<int> Commit()
        {
            return await context.SaveChangesAsync();
        }
    }
}