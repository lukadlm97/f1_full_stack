using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.PoweUnitSupplier
{
    public interface IPowerUnitSupplier : IRepository<PowerUnitSupplier>
    {
        Task<PowerUnitSupplier> GetById(int id);
    }
}