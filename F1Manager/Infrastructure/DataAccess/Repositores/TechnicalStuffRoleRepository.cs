using Domain.TechnicalStuffRole;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class TechnicalStuffRoleRepository : ITechnicalStuffRoleRepository
    {

        private readonly AppDbContext context;
        private readonly ILogger<TechnicalStuffRoleRepository> logger;

        public TechnicalStuffRoleRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<TechnicalStuffRoleRepository>();
        }
        public Task<IEnumerable<TechnicalStuffRole>> GetAll()
        {
            return ExecuteInTryCatch<IEnumerable<TechnicalStuffRole>>(async () =>
            {
                return this.context.TechnicalStuffRoles.ToList();
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
