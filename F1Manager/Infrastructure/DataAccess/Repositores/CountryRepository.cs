using Domain.Countries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext context;

        public CountryRepository(AppDbContext dbContext)
        {
            this.context = dbContext;
        }

        public async Task<bool> Delete(Country entity)
        {
            try
            {
                var forDelete = await context.Countries.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (forDelete == null)
                    return false;

                context.Countries.Remove(forDelete);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<List<Country>> GetAll()
        {
            try
            {
                return await context.Countries.ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<Country> GetById(int id)
        {
            try
            {
                return await context.Countries.FirstOrDefaultAsync(x=>x.Id==id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Insert(Country entity)
        {
            try
            {
                await context.Countries.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Country entity)
        {
            try
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return false;
            }
        }
    }
}