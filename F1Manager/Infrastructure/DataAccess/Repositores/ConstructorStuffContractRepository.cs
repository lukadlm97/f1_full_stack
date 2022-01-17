using Domain.ConstrucotrsStuffContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class ConstructorStuffContractRepository : IConstructorsStuffContractsRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<ConstructorStuffContractRepository> logger;

        public ConstructorStuffContractRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<ConstructorStuffContractRepository>();
        }
        public Task<bool> EndContract(ConstructorsStuffContracts parameters)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingConstract =  await context.ConstrucotrsStuffContracts.FirstOrDefaultAsync(x=>x.ConstructorId==parameters.ConstructorId&&x.TechnicalStuffId == parameters.TechnicalStuffId&&x.TechnicalStuffRoleId==parameters.TechnicalStuffRoleId);

                if(existingConstract == null)
                {
                    return false;
                }

                existingConstract.DateOfEnd=DateTime.Now;

                context.ConstrucotrsStuffContracts.Update(existingConstract);

                return true;

            }, "GetAll ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStuffContracts>> GetAll()
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStuffContracts>>(async () =>
            {
                return await context.ConstrucotrsStuffContracts.Include(x=>x.Constructor).Include(x=>x.TechnicalStuff)
                .Include(x=>x.TechnicalStuffRole).Where(x => x.DateOfEnd==null).ToListAsync();
            }, "GetAll ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStuffContracts>> GetAllForRoles(int roleId)
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStuffContracts>>(async () =>
            {
                return await context.ConstrucotrsStuffContracts.Where(x=>x.TechnicalStuffRoleId==roleId).Include(x => x.Constructor).Include(x => x.TechnicalStuff)
                .Include(x => x.TechnicalStuffRole).ToListAsync();
            }, "GetAllForRoles ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStuffContracts>> GetAllHistory()
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStuffContracts>>(async () =>
            {
                return await context.ConstrucotrsStuffContracts.Include(x => x.Constructor).Include(x => x.TechnicalStuff)
                .Include(x => x.TechnicalStuffRole).ToListAsync();
            }, "GetAllHistory ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStuffContracts>> GetCurrentPostion(int stuffId)
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStuffContracts>>(async () =>
            {
                return await context.ConstrucotrsStuffContracts.Include(x => x.Constructor).Include(x => x.TechnicalStuff)
                .Include(x => x.TechnicalStuffRole).Where(x => x.TechnicalStuffId == stuffId).ToListAsync();
            }, "GetCurrentPostion ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStuffContracts>> GetCurrentStuff(int constructorId)
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStuffContracts>>(async () =>
            {
                return await context.ConstrucotrsStuffContracts.Include(x => x.Constructor).Include(x => x.TechnicalStuff)
                .Include(x => x.TechnicalStuffRole).Where(x => x.ConstructorId == constructorId).ToListAsync();
            }, "GetCurrentStuff ConstructorsStuffContracts");
        }

        public Task<bool> InsertContract(ConstructorsStuffContracts parameters)
        {
            throw new NotImplementedException();
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
