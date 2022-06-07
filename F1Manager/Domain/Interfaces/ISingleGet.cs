using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISingleGet<T> where T : class
    {
        Task<T> GetById(int id);
    }
}