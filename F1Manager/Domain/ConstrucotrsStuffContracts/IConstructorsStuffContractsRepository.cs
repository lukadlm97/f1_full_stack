using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ConstrucotrsStuffContracts
{
    public interface IConstructorsStuffContractsRepository
    {
        Task<IEnumerable<ConstructorsStuffContracts>> GetAllHistory();
        Task<IEnumerable<ConstructorsStuffContracts>> GetAll();
        Task<bool> InsertContract(ConstructorsStuffContracts parameters);
        Task<bool> EndContract(ConstructorsStuffContracts parameters);
        Task<IEnumerable<ConstructorsStuffContracts>> GetCurrentStuff(int constructorId);
        Task<IEnumerable<ConstructorsStuffContracts>> GetCurrentPostion(int stuffId);
        Task<IEnumerable<ConstructorsStuffContracts>> GetAllForRoles(int roleId);
    }
}
