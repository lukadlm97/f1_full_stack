using Domain.TechnicalStuff;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class TechnicalStuffRepository : ITechnicalStuffRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<TechnicalStuffRepository> logger;

        public TechnicalStuffRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<TechnicalStuffRepository>();
        }
        public Task<bool> Delete(int id)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await context.TechnicalStuffs.FirstOrDefaultAsync(x => x.Id == id);
                if (forDelete == null)
                    return false;

                forDelete.IsDeleted = true;

                context.TechnicalStuffs.Update(forDelete);

                return true;
            }, "Delete error occured");
        }

        public Task<IEnumerable<TechnicalStuff>> GetAll()
        {
            return ExecuteInTryCatch<IEnumerable<TechnicalStuff>>(async () =>
            {
                return await context.TechnicalStuffs.Where(x=>!x.IsDeleted).ToListAsync();
            }, "GetAll TechnicalStuff");
        }

        public Task<TechnicalStuff> GetById(int id)
        {
            return ExecuteInTryCatch<TechnicalStuff>(async () =>
            {
                return await context.TechnicalStuffs.FirstOrDefaultAsync(x=>x.Id==id);
            }, "GetById TechnicalStuff");
        }

        public Task<bool> Insert(TechnicalStuff technicalStuff)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == technicalStuff.CountryId);

                if (country == null)
                    return false;

                technicalStuff.Country = country;
                var item = await context.TechnicalStuffs.AddAsync(technicalStuff);

                return true;
            }, "Insert TechnicalStuff");
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
