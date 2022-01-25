using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class ContractRepository : Domain.Contracts.IContractRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<ContractRepository> logger;

        public ContractRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<ContractRepository>();
        }

        public Task<bool> EndContract(int contractId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingConstract = await context.Contracts.FirstOrDefaultAsync(x => x.Id == contractId);

                if (existingConstract == null)
                {
                    return false;
                }

                existingConstract.EndOfContactDate = DateTime.Now;

                context.Contracts.Update(existingConstract);

                return true;
            }, "EndContract ContractRepository");
        }

        public Task<IEnumerable<Contract>> GetCurrentContracts(int constructorId)
        {
            return ExecuteInTryCatch<IEnumerable<Contract>>(async () =>
            {
                return await this.context.Contracts
                                .Where(x=>x.ConstructorId == constructorId && x.EndOfContactDate==null).ToListAsync();  
            }, "GetContracts on ContractRepository");
        }

        public Task<IEnumerable<Contract>> GetContractsHistory(int constructorId)
        {
            return ExecuteInTryCatch<IEnumerable<Contract>>(async () =>
            {
                return await this.context.Contracts
                                    .Where(x => x.ConstructorId == constructorId && x.EndOfContactDate != null).ToListAsync();
            }, "GetContracts on ContractRepository");
        }

        public Task<Contract> GetCurrentDriverContract(int driverId)
        {
            return ExecuteInTryCatch<Contract>(async () =>
            {
                return await this.context.Contracts
                                .Where(x => x.DriverId == driverId && x.EndOfContactDate == null).FirstOrDefaultAsync();
            }, "GetContracts on ContractRepository");
        }

        public Task<IEnumerable<Contract>> GetDriversContractHistory(int driverId)
        {
            return ExecuteInTryCatch<IEnumerable<Contract>>(async () =>
            {
                return await this.context.Contracts
                                .Where(x => x.DriverId == driverId && x.EndOfContactDate != null).ToListAsync();
            }, "GetContracts on ContractRepository");
        }

        public Task<bool> StartContract(Contract newContract)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var driver = await this.context.Drivers.FirstOrDefaultAsync(x=>x.Id==newContract.DriverId);
                var constructor = await this.context.Constructors.FirstOrDefaultAsync(x=>x.Id==newContract.ConstructorId);
                var driverRole = await this.context.DriverRoles.FirstOrDefaultAsync(x=>x.Id==newContract.DriverRolesId);

                if(driver == null || driverRole==null || constructor == null)
                {
                    return false;
                }

                newContract.Driver = driver;
                newContract.Constructor = constructor;
                newContract.DriverRoles = driverRole;
                newContract.StartOfContactDate=DateTime.Now;

                await context.Contracts.AddAsync(newContract);

                return true;
            }, "GetContracts on ContractRepository");
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

        public Task<bool> IsNotUnderContract(int driverId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                return await context.Contracts.Where(x => x.DriverId == driverId)
                                                            .AllAsync(y => y.EndOfContactDate != null);
            }, "IsStillUnderContract ConstructorsStuffContracts");
        }

        public Task<Contract> GetById(int contractId)
        {
            return ExecuteInTryCatch<Contract>(async () =>
            {
                return await this.context.Contracts.FirstOrDefaultAsync(x=>x.Id==contractId);
            }, "GetContracts on ContractRepository");
        }
    }
}
