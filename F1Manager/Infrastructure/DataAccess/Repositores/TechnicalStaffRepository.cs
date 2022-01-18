using Domain.TechnicalStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class TechnicalStaffRepository : ITechnicalStaffRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<TechnicalStaffRepository> logger;

        public TechnicalStaffRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<TechnicalStaffRepository>();
        }
        public Task<bool> Delete(int id)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await context.TechnicalStaffs.FirstOrDefaultAsync(x => x.Id == id);
                if (forDelete == null)
                    return false;

                forDelete.IsDeleted = true;

                context.TechnicalStaffs.Update(forDelete);

                return true;
            }, "Delete error occured");
        }

        public Task<IEnumerable<TechnicalStaff>> GetAll()
        {
            return ExecuteInTryCatch<IEnumerable<TechnicalStaff>>(async () =>
            {
                return await context.TechnicalStaffs.Where(x=>!x.IsDeleted).ToListAsync();
            }, "GetAll TechnicalStuff");
        }

        public Task<TechnicalStaff> GetById(int id)
        {
            return ExecuteInTryCatch<TechnicalStaff>(async () =>
            {
                return await context.TechnicalStaffs.FirstOrDefaultAsync(x=>x.Id==id);
            }, "GetById TechnicalStuff");
        }

        public Task<bool> Insert(TechnicalStaff technicalStuff)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == technicalStuff.CountryId);

                if (country == null)
                    return false;

                technicalStuff.Country = country;
                var item = await context.TechnicalStaffs.AddAsync(technicalStuff);

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
