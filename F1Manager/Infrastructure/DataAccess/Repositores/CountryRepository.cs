using Domain.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<CountryRepository> logger;

        public CountryRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.logger = loggerFactory.CreateLogger<CountryRepository>();
        }

        public Task<bool> Delete(Country entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await context.Countries.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (forDelete == null)
                    return false;

                context.Countries.Remove(forDelete);

                return true;
            }, "Delete Country");
        }

        public Task<List<Country>> GetAll()
        {
            return ExecuteInTryCatch<List<Country>>(async () =>
            {
                return await context.Countries.ToListAsync();
            }, "GetAll Country");
        }

        public Task<Country> GetById(int id)
        {
            return ExecuteInTryCatch<Country>(async () =>
            {
                return await context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            }, "GetById Country");
        }

        public Task<bool> Insert(Country entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                await context.Countries.AddAsync(entity);
                return true;
            }, "Insert Country");
        }

        public Task<bool> Update(Country entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingCountry = await this.context.Countries.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (existingCountry == null)
                    return false;

                existingCountry.KeggleId = entity.KeggleId;
                existingCountry.Name = entity.Name;
                existingCountry.NominalGDP = entity.NominalGDP;
                existingCountry.Population = entity.Population;
                existingCountry.ShareIfWorldGDP = entity.ShareIfWorldGDP;
                existingCountry.Code = entity.Code;
                existingCountry.GDPPerCapita = entity.GDPPerCapita;

                context.Countries.Update(existingCountry);
                return true;
            }, "Update Country");
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