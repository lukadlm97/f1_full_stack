using Domain.Countries;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.CountrySeed
{
    public class SeedData
    {
        public static void SeedCountriesData(AppDbContext context)
        {
            context.Countries.Add(new Domain.Countries.Country
            {
                Code = "T",
                GDPPerCapita = 110,
                KeggleId = -1,
                Name = "TEST",
                NominalGDP = 220,
                Population = 100000,
                ShareIfWorldGDP = 0.002m
            });
            context.SaveChanges();
        }

        private List<Country> GetData()
        {

            return null;
        }
    }
}
