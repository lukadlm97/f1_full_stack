using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TechnicalStuffRole
{
    public interface ITechnicalStuffRoleRepository
    {
        Task<IEnumerable<TechnicalStuffRole>> GetAll();
    }
}
