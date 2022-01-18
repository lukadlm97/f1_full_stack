using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.ConstructorsStaffContracts
{
    public interface IConstructorsStaffContractsUnitOfWork
    {
        Domain.ConstructorsStaffContracts.IConstructorsStaffContractsRepository ConstructorsStaffContracts { get; set; }

        Task<int> Commit();
    }
}
