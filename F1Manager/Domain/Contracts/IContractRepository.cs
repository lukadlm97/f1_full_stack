using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetCurrentContracts(int constructorId);

        Task<IEnumerable<Contract>> GetContractsHistory(int constructorId);

        Task<IEnumerable<Contract>> GetDriversContractHistory(int driverId);

        Task<Contract> GetCurrentDriverContract(int driverId);

        Task<bool> IsNotUnderContract(int driverId);

        Task<bool> StartContract(Contract newContract);

        Task<bool> EndContract(int contractId);
        Task<Contract> GetById(int contractId);
    }
}