using Domain.ConstructorsPowerUnits;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class ConstructorsPowerUnitRepository : IConstructorsPowerUnit
    {
        private readonly AppDbContext context;
        private readonly ILogger<ConstructorsPowerUnitRepository> logger;

        public ConstructorsPowerUnitRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<ConstructorsPowerUnitRepository>();
        }

        public Task<bool> ChangePowerUnitSupplier(int contractId, ConstructorPowerUnit constructorPowerUnit)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateNewContract(ConstructorPowerUnit newConstructorPowerUnit)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var originalSupplier = await context.PowerUnitSuppliers.FirstOrDefaultAsync(x => x.Id == newConstructorPowerUnit.PowerUnitSupplierId);
                var originalConstructor = await context.Constructors.FirstOrDefaultAsync(x => x.Id == newConstructorPowerUnit.ConstructorId);

                if (originalSupplier == null || originalConstructor == null)
                    return false;

                newConstructorPowerUnit.Constructor=originalConstructor;
                newConstructorPowerUnit.PowerUnitSupplier = originalSupplier;
                newConstructorPowerUnit.StartDate = DateTime.UtcNow;

                await context.AddAsync(newConstructorPowerUnit);

                return true;
            }, "GetAll Drivers");


        }

        public Task<bool> EndContract(int id)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingContract =  await context.ConstructorPowerUnits.FirstOrDefaultAsync(x => x.Id == id);

                existingContract.EndDate = DateTime.UtcNow;
                context.Update(existingContract);

                return true;
            }, "GetAll Drivers");
        }

        public Task<ConstructorPowerUnit> GetById(int id)
        {
            return ExecuteInTryCatch<ConstructorPowerUnit>(async () =>
            {
                return await context.ConstructorPowerUnits.FirstOrDefaultAsync(x => x.Id == id);

            }, "GetAll Drivers");
        }

        public Task<IEnumerable<ConstructorPowerUnit>> GetConstructorsHistoryPowerUnit(int constructorId)
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorPowerUnit>>(async () =>
            {
                return await context.ConstructorPowerUnits.Where(x => x.ConstructorId == constructorId && x.EndDate!=null).ToListAsync();

            }, "GetAll Drivers");
        }
        

        public Task<ConstructorPowerUnit> GetCurrentConstructorPowerUnit(int constructorId)
        {
            return ExecuteInTryCatch<ConstructorPowerUnit>(async () =>
            {
                return await context.ConstructorPowerUnits.FirstOrDefaultAsync(x => x.ConstructorId == constructorId && x.EndDate==null);
              
            }, "GetAll Drivers");
        }

        public Task<bool> IsInContract(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                return await context.ConstructorPowerUnits.Where(x => x.ConstructorId == constructorId)
                                                            .AnyAsync(y => y.EndDate == null);
            }, "IsStillUnderContract ConstructorsStuffContracts");
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
