using Domain.Drivers;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Drivers
{
    public interface IDriversUnitOfWork
    {
        IDriverRepository Drivers { get; set; }

        Task<int> Commit();
    }
}