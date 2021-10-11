using Domain.Countries;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Countries
{
    public class CountriesUoW : ICountriesUnitOfWork
    {
        private readonly AppDbContext context;

        public CountriesUoW(AppDbContext dbContext)
        {
            this.context = dbContext;
            Countries = new CountryRepository(dbContext);
        }

        public ICountryRepository Countries { get; set; }

        public Task<int> Commit()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}