using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.TechnicalStaff
{
    public interface ITechnicalStaffRepository
    {
        Task<IEnumerable<TechnicalStaff>> GetAll();
        Task<TechnicalStaff> GetById(int id);

        Task<bool> Insert(TechnicalStaff technicalStuff);

        Task<bool> Delete(int id);
    }
}