using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TechnicalStaffRole
{
    public interface ITechnicalStaffRoleRepository
    {
        Task<IEnumerable<Domain.TechnicalStaffRole.TechnicalStaffRole>> GetAll();
    }
}
