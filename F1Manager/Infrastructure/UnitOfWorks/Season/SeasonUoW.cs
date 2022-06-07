using Infrastructure.DataAccess;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Season
{
    public class SeasonUoW : ISeasonUnitOfWork
    {
        private readonly AppDbContext context;

        public SeasonUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            SeasonRepository = new Infrastructure.DataAccess.Repositores.SeasonRepository(dbContext, loggerFactory);
        }

        public Domain.Season.ISeasonRepository SeasonRepository { get; set; }

        public async Task<int> Commit()
        {
            return await context.SaveChangesAsync();
        }
    }
}