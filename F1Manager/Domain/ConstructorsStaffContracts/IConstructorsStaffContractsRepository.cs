using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ConstructorsStaffContracts
{
    public interface IConstructorsStaffContractsRepository
    {
        Task<IEnumerable<ConstructorsStaffContracts>> GetAllHistory();
        Task<IEnumerable<ConstructorsStaffContracts>> GetAll();
        Task<ConstructorsStaffContracts> GetById(int id);
        Task<bool> InsertContract(ConstructorsStaffContracts parameters);
        Task<bool> EndContract(int contractId);
        Task<bool> IsNotUnderContract(int staffId);
        Task<IEnumerable<ConstructorsStaffContracts>> GetCurrentStuff(int constructorId);
        Task<IEnumerable<ConstructorsStaffContracts>> GetHistoryStuff(int constructorId);
        Task<ConstructorsStaffContracts> GetCurrentPosition(int stuffId);
        Task<IEnumerable<ConstructorsStaffContracts>> GetHistoryPosition(int staffId);
        Task<IEnumerable<ConstructorsStaffContracts>> GetAllForRoles(int roleId);
    }
}
