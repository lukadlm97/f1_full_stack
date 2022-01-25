using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.ConstructorsPowerUnit
{
    public interface IConstructorsPowerUnit
    {
        Domain.ConstructorsPowerUnits.IConstructorsPowerUnit ConstructorsPowerUnit { get; set; }

        Task<int> Commit();
    }
}
