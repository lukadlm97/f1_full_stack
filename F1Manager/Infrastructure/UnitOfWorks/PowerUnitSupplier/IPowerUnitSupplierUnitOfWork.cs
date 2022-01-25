using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.PowerUnitSupplier
{
    public interface IPowerUnitSupplierUnitOfWork
    {
        Domain.PoweUnitSupplier.IPowerUnitSupplier PowerUnitSupplier { get; set; }

        Task<int> Commit();
    }
}
