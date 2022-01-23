using Domain.RacingChampionship;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Competition
{
    public class CompetitionUoW : ICompetitionUnitOfWork
    {
        private readonly AppDbContext context;

        public CompetitionUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            CompetitionRepository = new RacingChampionshipRepository(dbContext, loggerFactory);
        }
        public IRacingChampionshipRepository CompetitionRepository { get; set ; }

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
