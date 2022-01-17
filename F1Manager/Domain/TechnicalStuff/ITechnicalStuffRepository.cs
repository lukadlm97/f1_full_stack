using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.TechnicalStuff
{
    public interface ITechnicalStuffRepository
    {
        Task<IEnumerable<TechnicalStuff>> GetAll();
        Task<TechnicalStuff> GetById(int id);

        Task<bool> Insert(TechnicalStuff technicalStuff);

        Task<bool> Delete(int id);
    }
}