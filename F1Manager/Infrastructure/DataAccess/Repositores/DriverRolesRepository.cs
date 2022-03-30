using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DriverRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DataAccess.Repositores
{
    public class DriverRolesRepository:IDriverRolesRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<DriverRolesRepository> logger;

        public DriverRolesRepository(AppDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.logger = loggerFactory.CreateLogger<DriverRolesRepository>();
        }
        public Task<List<DriverRoles>> GetAll()
        {
            return ExecuteInTryCatch<List<DriverRoles>>(async () =>
            {
                return await context.DriverRoles.ToListAsync();
            }, "GetAll DriverRoles");
        }

        public Task<bool> Insert(DriverRoles entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(DriverRoles entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(DriverRoles entity)
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
