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

        public ConstructorRacingDetailRepository(AppDbContext appContext, ILogger<ConstructorRacingDetailRepository> logger)
        {
            this.appContext = appContext;
            this.logger = logger;
        }

        public Task<bool> Delete(ConstructorsRacingDetail entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await this.appContext.ConstructorsRacingDetails.FirstOrDefaultAsync(x => x.Id == entity.Id);
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

        public Task<bool> Insert(ConstructorsRacingDetail entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var originalConstructor = await this.appContext.Constructors
                                                    .FirstOrDefaultAsync(x => x.Id == entity.ConstructorId);

                if (originalConstructor == null)
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
                                                                        .FirstOrDefaultAsync(x => x.Id == entity.Id);
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