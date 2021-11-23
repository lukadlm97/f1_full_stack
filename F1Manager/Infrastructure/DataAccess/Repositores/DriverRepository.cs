using Domain.Drivers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext context;

        public DriverRepository(AppDbContext dbContext)
        {
            this.context = dbContext;
        }

        public async Task<bool> Delete(Driver entity)
        {
            try
            {
                var forDelete = await context.Drivers.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (forDelete == null)
                    return false;

                context.Drivers.Remove(forDelete);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<List<Driver>> GetAll()
        {
            try
            {
                return await context.Drivers.Include(x=>x.Country).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<Driver> GetById(int id)
        {

            try
            {
                return await context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Driver>> GetByNationality(string countryName = null, int countryId = 0)
        {
            try
            {
                var existingCountry = countryId != default ? await context.Countries.FirstOrDefaultAsync(x => x.Id == countryId) :
                      await context.Countries.FirstOrDefaultAsync(x => x.Name.ToLower().Contains(countryName.ToLower()));

                if (existingCountry == null)
                    return null;


               return context.Drivers.Where(x=>x.Country.Id==existingCountry.Id).AsEnumerable();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return new List<Driver>();
            }
        }

        public async Task<bool> Insert(Driver entity)
        {
            try
            {
                var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == entity.CountryId);

                if (country == null)
                    return false;

                entity.Country = country;
                await context.Drivers.AddAsync(entity);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Driver entity)
        {
            try
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return false;
            }
        }
    }
}
