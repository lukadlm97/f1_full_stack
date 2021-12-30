using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class RepositoryBase
    {
        private readonly ILogger logger;

        public RepositoryBase(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<T> ExecuteInTryCatch<T>(Func<Task<T>> databaseFunction, string errorMessage)
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