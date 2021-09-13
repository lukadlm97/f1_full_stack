using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormulaManager.DAL.Entities.Repositories
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<T> Create(T entity);

        Task<T> Update(int id, T entity);

        Task<bool> Delete(int id);
    }
}