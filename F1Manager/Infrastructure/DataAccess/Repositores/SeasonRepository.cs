using Domain.Season;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class SeasonRepository : RepositoryBase, Domain.Season.ISeasonRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SeasonRepository> logger;

        public SeasonRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<SeasonRepository>())
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<SeasonRepository>();
        }

        public Task<List<Season>> GetAll()
        {
            return ExecuteInTryCatch<List<Season>>(async () =>
            {
                return await this.context.Seasons.ToListAsync();
            }, "GetAll Season");
        }

        public Task<bool> Insert(Season entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var racingChampionship = await context.RacingChampionships.FirstOrDefaultAsync(x => x.Id == entity.RacingChampionshipId);

                if (racingChampionship == null)
                    return false;

                entity.RacingChampionship = racingChampionship;
                await context.Seasons.AddAsync(entity);

                return true;
            }, "Insert Season");
        }

        public Task<bool> Update(Season entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forChange = await context.Seasons.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (forChange == null)
                    return false;

                forChange.FullName = entity.FullName;
                forChange.ShortName = entity.ShortName;
                var racingChampionship = await context.RacingChampionships.FirstOrDefaultAsync(x => x.Id == entity.RacingChampionshipId);

                if (racingChampionship == null)
                    return false;

                entity.RacingChampionship = racingChampionship;
                context.Seasons.Update(forChange);

                return true;
            }, "edit error occurred");
        }

        public Task<bool> Delete(Season entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await context.Seasons.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (forDelete == null)
                    return false;

                context.Seasons.Remove(forDelete);

                return true;
            }, "Delete error occurred");
        }

        public Task<Season> GetById(int id)
        {
            return ExecuteInTryCatch<Season>(async () =>
            {
                var selectedSeason = await context.Seasons.FirstOrDefaultAsync(x => x.Id == id);
                if (selectedSeason == null)
                    throw new Exception("Not found season");

                return selectedSeason;
            }, "Got single season error");
        }
    }
}