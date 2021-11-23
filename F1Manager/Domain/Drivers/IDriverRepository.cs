using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Drivers
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver> GetById(int id);
        Task<IEnumerable<Driver>> GetByNationality(string countryName = default, int countryId = default);
    }
}