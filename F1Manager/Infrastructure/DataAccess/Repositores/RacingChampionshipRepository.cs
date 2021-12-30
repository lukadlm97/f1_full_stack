using Domain.RacingChampionship;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class RacingChampionshipRepository : RepositoryBase, IRacingChampionshipRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<CountryRepository> logger;

        public RacingChampionshipRepository(AppDbContext dbContext, ILoggerFactory loggerFactory):base(loggerFactory.CreateLogger<CountryRepository>())
        {
            this.context = dbContext;
        }

        public Task<bool> Delete(RacingChampionship entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await context.RacingChampionships.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (forDelete == null)
                    return false;

                context.RacingChampionships.Remove(forDelete);

                return true;
            }, "Delete RacingChampionship");
        }

        public Task<List<RacingChampionship>> GetAll()
        {
            return ExecuteInTryCatch<List<RacingChampionship>>(async () =>
            {
                return await context.RacingChampionships.ToListAsync();
            }, "GetAll RacingChampionship");
        }

        public Task<bool> Insert(RacingChampionship entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                await context.RacingChampionships.AddAsync(entity);
                return true;
            }, "Insert RacingChampionship");
        }

        public Task<bool> Update(RacingChampionship entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingRacingChampionship = await this.context.RacingChampionships.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (existingRacingChampionship == null)
                    return false;

                existingRacingChampionship.ChampionshipNameShort = entity.ChampionshipNameShort;
                existingRacingChampionship.ChampionshipNameFull = entity.ChampionshipNameFull;
                existingRacingChampionship.LastEntry = entity.LastEntry;
                existingRacingChampionship.FirstEntry = entity.FirstEntry;
                existingRacingChampionship.OrganisedBy = entity.OrganisedBy;
                existingRacingChampionship.TotalCompetitions = entity.TotalCompetitions;

               

                context.RacingChampionships.Update(existingRacingChampionship);
                return true;
            }, "Update RacingChampionship");
        }
    }
}
