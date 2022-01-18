using Domain.Constructors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class ConstructorRepository : IConstructorRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<ConstructorRepository> logger;

        public ConstructorRepository(AppDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.logger = loggerFactory.CreateLogger<ConstructorRepository>();
        }

        public Task<bool> ChangeTeamNationality(int constructorId, int teamNationalityId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingConstructor = await this.context.Constructors.FirstOrDefaultAsync(x => x.Id == constructorId);
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == teamNationalityId);

                if (existingConstructor == null || country == null)
                    return false;

                existingConstructor.Country = country;
                existingConstructor.UpdatedDate= DateTime.UtcNow;

                context.Constructors.Update(existingConstructor);
                return true;
            }, "Update Constructors");
        }

        public Task<bool> Delete(Constructor entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await context.Constructors.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (forDelete == null)
                    return false;

                forDelete.IsDeleted = true;

                context.Constructors.Update(forDelete);

                return true;
            }, "Delete Constructors");
        }

        public Task<List<Constructor>> GetAll()
        {
            return ExecuteInTryCatch<List<Constructor>>(async () =>
            {
                return await this.context.Constructors.Where(x=>!x.IsDeleted && x.IsActive).ToListAsync();
            }, "GetAll Constructors");
        }

        public Task<IEnumerable<Constructor>> GetConstructors(int countryId)
        {
            return ExecuteInTryCatch<IEnumerable<Constructor>>(async () =>
            {
                return await this.context.Constructors.Where(x => x.CountryId == countryId).ToListAsync();
            }, "GetAll Constructors");
        }

        public Task<bool> Insert(Constructor entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == entity.CountryId);

                if (country == null)
                    return false;

                entity.Country = country;
                entity.CreatedDate = DateTime.UtcNow;
                await context.Constructors.AddAsync(entity);

                return true;
            }, "Insert Constructors");
        }

        public Task<bool> MakeComeback(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingConstructor = await this.context.Constructors.FirstOrDefaultAsync(x => x.Id == constructorId);

                if (existingConstructor == null)
                    return false;

                existingConstructor.UpdatedDate = DateTime.UtcNow;
                existingConstructor.IsActive = true;

                context.Constructors.Update(existingConstructor);
                return true;
            }, "Update Constructors");
        }

        public Task<bool> RetireConstructor(int constructorId)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingConstructor = await this.context.Constructors.FirstOrDefaultAsync(x => x.Id == constructorId);

                if (existingConstructor == null)
                    return false;

                existingConstructor.UpdatedDate = DateTime.UtcNow;
                existingConstructor.IsActive = false;

                context.Constructors.Update(existingConstructor);
                return true;
            }, "Update Constructors");
        }

        public Task<bool> Update(Constructor entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingConstructor = await this.context.Constructors.FirstOrDefaultAsync(x => x.Id == entity.Id);
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == entity.CountryId);

                if (existingConstructor == null || country == null)
                    return false;

                existingConstructor.UpdatedDate = DateTime.UtcNow;
                existingConstructor.Base = entity.Base;
                existingConstructor.FullName = entity.FullName;
                existingConstructor.ShortName = entity.ShortName;
                existingConstructor.FirstEntry = entity.FirstEntry;
                existingConstructor.LastEntry = entity.LastEntry;
                existingConstructor.Website = entity.Website;
                existingConstructor.UpdatedBy = entity.UpdatedBy;
                existingConstructor.UpdatedDate = entity.UpdatedDate;
                existingConstructor.Country = country;

                context.Constructors.Update(existingConstructor);
                return true;
            }, "Update Constructors");
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