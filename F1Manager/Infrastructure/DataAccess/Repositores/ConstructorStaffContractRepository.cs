using Domain.ConstructorsStaffContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class ConstructorStaffContractRepository : IConstructorsStaffContractsRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<ConstructorStaffContractRepository> logger;

        public ConstructorStaffContractRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<ConstructorStaffContractRepository>();
        }

        public Task<bool> EndContract(ConstructorsStaffContracts parameters)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingConstract = await context.ConstructorsStaffContracts.FirstOrDefaultAsync(x => x.ConstructorId == parameters.ConstructorId && x.TechnicalStaffId == parameters.TechnicalStaffId && x.TechnicalStaffRoleId == parameters.TechnicalStaffRoleId);

                if (existingConstract == null)
                {
                    return false;
                }

                existingConstract.DateOfEnd = DateTime.Now;

                context.ConstructorsStaffContracts.Update(existingConstract);

                return true;
            }, "GetAll ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStaffContracts>> GetAll()
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStaffContracts>>(async () =>
            {
                return await context.ConstructorsStaffContracts.Include(x => x.Constructor).Include(x => x.TechnicalStaff)
                .Include(x => x.TechnicalStaffRole).Where(x => x.DateOfEnd == null).ToListAsync();
            }, "GetAll ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStaffContracts>> GetAllForRoles(int roleId)
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStaffContracts>>(async () =>
            {
                return await context.ConstructorsStaffContracts.Where(x => x.TechnicalStaffRoleId == roleId && x.DateOfEnd==null).Include(x => x.Constructor).Include(x => x.TechnicalStaff)
                .Include(x => x.TechnicalStaffRole).ToListAsync();
            }, "GetAllForRoles ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStaffContracts>> GetAllHistory()
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStaffContracts>>(async () =>
            {
                return await context.ConstructorsStaffContracts.Include(x => x.Constructor).Include(x => x.TechnicalStaff)
                .Include(x => x.TechnicalStaffRole).ToListAsync();
            }, "GetAllHistory ConstructorsStuffContracts");
        }

        public Task<ConstructorsStaffContracts> GetCurrentPosition(int stuffId)
        {
            return ExecuteInTryCatch<ConstructorsStaffContracts>(async () =>
            {
                return await context.ConstructorsStaffContracts.Where(x=>x.DateOfEnd==null).Include(x => x.Constructor).Include(x => x.TechnicalStaff)
                .Include(x => x.TechnicalStaffRole).FirstOrDefaultAsync(x => x.TechnicalStaffId == stuffId);
            }, "GetCurrentPostion ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStaffContracts>> GetCurrentStuff(int constructorId)
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStaffContracts>>(async () =>
            {
                return await context.ConstructorsStaffContracts.Include(x => x.Constructor).Include(x => x.TechnicalStaff)
                .Include(x => x.TechnicalStaffRole).Where(x => x.ConstructorId == constructorId && x.DateOfEnd==null).ToListAsync();
            }, "GetCurrentStuff ConstructorsStuffContracts");
        }

        public Task<ConstructorsStaffContracts> GetById(int id)
        {
            return ExecuteInTryCatch<ConstructorsStaffContracts>(async () =>
            {
                return await context.ConstructorsStaffContracts.Include(x => x.Constructor).Include(x => x.TechnicalStaff)
                .Include(x => x.TechnicalStaffRole).FirstOrDefaultAsync(x => x.Id == id);
            }, "GetCurrentStuff ConstructorsStuffContracts");
        }

        public Task<bool> InsertContract(ConstructorsStaffContracts parameters)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var stuff =
                        await context.TechnicalStaffs.FirstOrDefaultAsync(x => x.Id == parameters.TechnicalStaffId);
                var stuffRole =
                        await context.TechnicalStaffRoles.FirstOrDefaultAsync(x => x.Id == parameters.TechnicalStaffRoleId);
                var constructor =
                        await context.Constructors.FirstOrDefaultAsync(x => x.Id == parameters.ConstructorId);

                if (stuff == null || stuffRole == null || constructor == null)
                {
                    return false;
                }

                parameters.TechnicalStaff = stuff;
                parameters.TechnicalStaffRole = stuffRole;
                parameters.Constructor = constructor;
                parameters.DateOfSign = DateTime.Now;

                await context.ConstructorsStaffContracts.AddAsync(parameters);

                return true;
            }, "InsertContract ConstructorsStuffContracts");
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

        public Task<IEnumerable<ConstructorsStaffContracts>> GetHistoryPosition(int staffId)
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStaffContracts>>(async () =>
            {
                return await context.ConstructorsStaffContracts.Include(x => x.Constructor).Include(x => x.TechnicalStaff)
                .Include(x => x.TechnicalStaffRole).Where(x=>x.TechnicalStaffId==staffId&&x.DateOfEnd!=null).ToListAsync();
            }, "GetAllHistory ConstructorsStuffContracts");
        }

        public Task<IEnumerable<ConstructorsStaffContracts>> GetHistoryStuff(int constructorId)
        {
            return ExecuteInTryCatch<IEnumerable<ConstructorsStaffContracts>>(async () =>
            {
                return await context.ConstructorsStaffContracts.Include(x => x.Constructor).Include(x => x.TechnicalStaff)
                .Include(x => x.TechnicalStaffRole).Where(x => x.ConstructorId == constructorId && x.DateOfEnd != null).ToListAsync();
            }, "GetCurrentStuff ConstructorsStuffContracts");
        }
    }
}