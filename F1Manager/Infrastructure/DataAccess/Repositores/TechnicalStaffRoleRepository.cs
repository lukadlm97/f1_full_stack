using Domain.TechnicalStaffRole;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class TechnicalStaffRoleRepository : ITechnicalStaffRoleRepository
    {

        private readonly AppDbContext context;
        private readonly ILogger<TechnicalStaffRoleRepository> logger;

        public TechnicalStaffRoleRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<TechnicalStaffRoleRepository>();
        }
        public Task<IEnumerable<TechnicalStaffRole>> GetAll()
        {
            return ExecuteInTryCatch<IEnumerable<TechnicalStaffRole>>(async () =>
            {
                return this.context.TechnicalStaffRoles.ToList();
            }, "GetAll Roles");
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
