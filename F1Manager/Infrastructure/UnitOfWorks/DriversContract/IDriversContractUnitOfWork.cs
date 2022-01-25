using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.DriversContract
{
   
    public interface IDriversContractUnitOfWork
    {
        IContractRepository DriversContract { get; set; }

        Task<int> Commit();
    }
}
