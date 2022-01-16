using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Drivers
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver> GetById(int id);

        Task<IEnumerable<Driver>> GetByNationality(string countryName = default, int countryId = default);

        Task<IEnumerable<Driver>> GetAllRaw(CancellationToken cancellationToken = default);

        Task<bool> DriverRetirement(int id, CancellationToken cancellationToken = default);
        Task<bool> DriverReactivation(int id, CancellationToken cancellationToken = default);
        Task<bool> ChangeCitizenship(int id, int countryId,CancellationToken cancellationToken = default);
        Task<Driver> GetLatestCreated(CancellationToken cancellationToken = default);
    }
}