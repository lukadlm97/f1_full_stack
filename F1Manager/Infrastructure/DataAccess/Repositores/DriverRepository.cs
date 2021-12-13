using Domain.Drivers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<DriverRepository> logger;

        public DriverRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<DriverRepository>();
        }

        public Task<bool> Delete(Driver entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await context.Drivers.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (forDelete == null)
                    return false;

                context.Drivers.Remove(forDelete);

                return true;
            }, "Delete Driver");
        }

        public Task<List<Driver>> GetAll()
        {
            return ExecuteInTryCatch<List<Driver>>(async () =>
            {
                return await context.Drivers.Where(x=>x.IsActive).Include(x => x.Country).ToListAsync();
            }, "GetAll Drivers");
        }

        public Task<Driver> GetById(int id)
        {
            return ExecuteInTryCatch<Driver>(async () =>
            {
                return await context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            }, "GetById Driver");
        }

        public Task<IEnumerable<Driver>> GetByNationality(string countryName = null, int countryId = 0)
        {
            return ExecuteInTryCatch<IEnumerable<Driver>>(async () =>
            {
                var existingCountry = countryId != default ? await context.Countries.FirstOrDefaultAsync(x => x.Id == countryId) :
                        await context.Countries.FirstOrDefaultAsync(x => x.Name.ToLower().Contains(countryName.ToLower()));

                if (existingCountry == null)
                    return null;

                return context.Drivers.Where(x => x.Country.Id == existingCountry.Id).AsEnumerable();
            }, "GetByNationality Driver");
        }

        public Task<bool> Insert(Driver entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == entity.CountryId);

                if (country == null)
                    return false;

                entity.Country = country;
                await context.Drivers.AddAsync(entity);

                return true;
            }, "Insert Driver");
        }

        public Task<bool> Update(Driver entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingDriver = await this.context.Drivers.FirstOrDefaultAsync(x => x.Id == entity.Id);
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == entity.CountryId);

                if (existingDriver == null || country == null)
                    return false;

                existingDriver.KeggleId = entity.KeggleId;
                existingDriver.Country = country;
                existingDriver.DateOfBirth = entity.DateOfBirth;
                existingDriver.DriverRef = entity.DriverRef;
                existingDriver.Code = entity.Code;
                existingDriver.Forename = entity.Forename;
                existingDriver.Surname = entity.Surname;

                context.Drivers.Update(existingDriver);
                return true;
            }, "Update Driver");
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