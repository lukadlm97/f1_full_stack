using Domain.PoweUnitSupplier;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class PowerUnitSupplierRepository : Domain.PoweUnitSupplier.IPowerUnitSupplier
    {
        private readonly AppDbContext context;
        private readonly ILogger<PowerUnitSupplierRepository> logger;

        public PowerUnitSupplierRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<PowerUnitSupplierRepository>();
        }

        public Task<bool> Delete(PowerUnitSupplier entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingPowerUnitSupplier = await context.PowerUnitSuppliers.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (existingPowerUnitSupplier == null)
                {
                    return false;
                }

                existingPowerUnitSupplier.IsActive = false;
                context.PowerUnitSuppliers.Update(existingPowerUnitSupplier);

                return true;
            }, "GetAll Drivers");
        }

        public Task<List<PowerUnitSupplier>> GetAll()
        {
            return ExecuteInTryCatch<List<PowerUnitSupplier>>(async () =>
            {
                return context.PowerUnitSuppliers.Where(x=>x.IsActive).ToList();
            }, "GetAll Drivers");
        }

        public Task<PowerUnitSupplier> GetById(int id)
        {
            return ExecuteInTryCatch<PowerUnitSupplier>(async () =>
            {
                return await context.PowerUnitSuppliers.FirstOrDefaultAsync(x => x.Id == id);
            }, "GetAll Drivers");
        }

        public Task<bool> Insert(PowerUnitSupplier entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == entity.CountryId);

                if(country == null)
                {
                    return false;
                }
                entity.Country = country;

                await context.PowerUnitSuppliers.AddAsync(entity);

                return true;
            }, "GetAll Drivers");
        }

        public Task<bool> Update(PowerUnitSupplier entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingPowerUnitSupplier = await context.PowerUnitSuppliers.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (existingPowerUnitSupplier == null)
                {
                    return false;
                }
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == entity.CountryId);

                if (country == null)
                {
                    return false;
                }

                existingPowerUnitSupplier.Country = country;
                existingPowerUnitSupplier.IsActive = entity.IsActive;
                existingPowerUnitSupplier.SupplierName = entity.SupplierName;
                context.PowerUnitSuppliers.Update(existingPowerUnitSupplier);

                return true;
            }, "GetAll Drivers");
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