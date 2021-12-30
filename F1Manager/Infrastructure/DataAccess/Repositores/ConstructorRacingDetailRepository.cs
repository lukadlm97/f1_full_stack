using Domain.ConstructorRacingDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class ConstructorRacingDetailRepository : IConstructorRacingDetail
    {
        private readonly AppDbContext appContext;
        private readonly ILogger<ConstructorRacingDetailRepository> logger;

        public ConstructorRacingDetailRepository(AppDbContext appContext, ILoggerFactory loggerFactory)
        {
            this.appContext = appContext;
            this.logger = loggerFactory.CreateLogger<ConstructorRacingDetailRepository>();
        }

        public Task<bool> ChangeToApproprateConstructor(int constructorId, int newConstructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingConstructorRacingDetails = await this.appContext.ConstructorsRacingDetails
                                                                        .FirstOrDefaultAsync(x => x.ConstructorId == constructorId);
                var originalConstructor = await appContext.Constructors.FirstOrDefaultAsync(x => x.Id == newConstructorId);

                if (existingConstructorRacingDetails == null || originalConstructor == null)
                    return false;

                existingConstructorRacingDetails.Constructor = originalConstructor;

                this.appContext.ConstructorsRacingDetails.Update(existingConstructorRacingDetails);
                return true;
            }, "Update ConstructorsRacingDetails");
        }

        public Task<bool> CreateInitState(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var originalConstructor = await this.appContext.Constructors
                                                    .FirstOrDefaultAsync(x => x.Id == constructorId);
                var existRacingDetails = await  this.appContext.ConstructorsRacingDetails
                                                    .FirstOrDefaultAsync(x => x.ConstructorId == constructorId);

                if (originalConstructor == null || existRacingDetails!=null)
                    return false;

                var entity = new ConstructorsRacingDetail() { Constructor=originalConstructor};
                await this.appContext.ConstructorsRacingDetails.AddAsync(entity);

                return true;
            }, "Insert ConstructorsRacingDetails");
        }

        public Task<bool> Delete(ConstructorsRacingDetail entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await this.appContext.ConstructorsRacingDetails.FirstOrDefaultAsync(x => x.ConstructorId == entity.ConstructorId);
                if (forDelete == null)
                    return false;

                this.appContext.ConstructorsRacingDetails.Remove(forDelete);

                return true;
            }, "Delete ConstructorsRacingDetails");
        }

        public Task<List<ConstructorsRacingDetail>> GetAll()
        {
            return ExecuteInTryCatch<List<ConstructorsRacingDetail>>(async () =>
            {
                return await this.appContext.ConstructorsRacingDetails.ToListAsync();
            }, "GetAll ConstructorsRacingDetails");
        }

        public Task<bool> IncrementConstructorChampionships(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var racingDetails = await this.appContext.ConstructorsRacingDetails
                                                    .FirstOrDefaultAsync(x => x.ConstructorId == constructorId);

                if (racingDetails == null)
                    return false;

                racingDetails.ConstructorChampionships++;
                this.appContext.ConstructorsRacingDetails.Update(racingDetails);

                return true;
            }, "Increment Constructor Championships on ConstructorsRacingDetails");
        }

        public Task<bool> IncrementDriverChampionships(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var racingDetails = await this.appContext.ConstructorsRacingDetails
                                                    .FirstOrDefaultAsync(x => x.ConstructorId == constructorId);

                if (racingDetails == null)
                    return false;

                racingDetails.DriverChampionships++;
                this.appContext.ConstructorsRacingDetails.Update(racingDetails);

                return true;
            }, "Increment Driver Championships on ConstructorsRacingDetails");
        }

        public Task<bool> IncrementFastesLaps(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var racingDetails = await this.appContext.ConstructorsRacingDetails
                                                    .FirstOrDefaultAsync(x => x.ConstructorId == constructorId);

                if (racingDetails == null)
                    return false;

                racingDetails.FastesLaps++;
                this.appContext.ConstructorsRacingDetails.Update(racingDetails);

                return true;
            }, "Increment Fastes Laps on ConstructorsRacingDetails");
        }

        public Task<bool> IncrementPodiums(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var racingDetails = await this.appContext.ConstructorsRacingDetails
                                                    .FirstOrDefaultAsync(x => x.ConstructorId == constructorId);

                if (racingDetails == null)
                    return false;

                racingDetails.Podiums++;
                this.appContext.ConstructorsRacingDetails.Update(racingDetails);

                return true;
            }, "Increment Podiums on ConstructorsRacingDetails");
        }

        public Task<bool> IncrementPolPositions(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var racingDetails = await this.appContext.ConstructorsRacingDetails
                                                    .FirstOrDefaultAsync(x => x.ConstructorId == constructorId);

                if (racingDetails == null)
                    return false;

                racingDetails.PolPositions++;
                this.appContext.ConstructorsRacingDetails.Update(racingDetails);

                return true;
            }, "Increment Pol Positions on ConstructorsRacingDetails");
        }

        public Task<bool> IncrementRaceVictories(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var racingDetails = await this.appContext.ConstructorsRacingDetails
                                                    .FirstOrDefaultAsync(x => x.ConstructorId == constructorId);

                if (racingDetails == null)
                    return false;

                racingDetails.RaceVictories++;
                this.appContext.ConstructorsRacingDetails.Update(racingDetails);

                return true;
            }, "Increment Race Victories on ConstructorsRacingDetails");
        }

        public Task<bool> Insert(ConstructorsRacingDetail entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var originalConstructor = await this.appContext.Constructors
                                                    .FirstOrDefaultAsync(x => x.Id == entity.ConstructorId);
                var existRacingDetails = await this.appContext.ConstructorsRacingDetails
                                                  .FirstOrDefaultAsync(x => x.ConstructorId == entity.ConstructorId);

                if (originalConstructor == null || existRacingDetails!= null)
                    return false;

                entity.Constructor = originalConstructor;
                await this.appContext.ConstructorsRacingDetails.AddAsync(entity);

                return true;
            }, "Insert ConstructorsRacingDetails");
        }

        public Task<bool> Update(ConstructorsRacingDetail entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingConstructorRacingDetails = await this.appContext.ConstructorsRacingDetails
                                                                        .FirstOrDefaultAsync(x => x.ConstructorId == entity.ConstructorId);
                var originalConstructor = await appContext.Constructors.FirstOrDefaultAsync(x => x.Id == entity.ConstructorId);

                if (existingConstructorRacingDetails == null || originalConstructor == null)
                    return false;

                existingConstructorRacingDetails.PolPositions = entity.PolPositions;
                existingConstructorRacingDetails.RaceVictories = entity.RaceVictories;
                existingConstructorRacingDetails.FastesLaps = entity.FastesLaps;
                existingConstructorRacingDetails.DriverChampionships = entity.DriverChampionships;
                existingConstructorRacingDetails.ConstructorChampionships = entity.ConstructorChampionships;
                existingConstructorRacingDetails.Podiums = entity.Podiums;
                existingConstructorRacingDetails.Constructor = originalConstructor;

                this.appContext.ConstructorsRacingDetails.Update(existingConstructorRacingDetails);
                return true;
            }, "Update ConstructorsRacingDetails");
        }

        private Task<T> ExecuteInTryCatch<T>(Func<Task<T>> databaseFunction, string errorMessage)
        {
            try
            {
                return databaseFunction();
            }
            catch (Exception e)
            {
                logger.LogError(e, errorMessage);
                throw;
            }
        }
    }
}