using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.ConstructorsPowerUnits
{
    public interface IConstructorsPowerUnit
    {
        Task<bool> CreateNewContract(ConstructorPowerUnit newConstructorPowerUnit);
        Task<bool> EndContract(int id);
        Task<bool> IsInContract(int constructorId);
        Task<ConstructorPowerUnit> GetById(int id);
        Task<ConstructorPowerUnit> GetCurrentConstructorPowerUnit(int constructorId);
        Task<IEnumerable<ConstructorPowerUnit>> GetConstructorsHistoryPowerUnit(int constructorId);
    }
}