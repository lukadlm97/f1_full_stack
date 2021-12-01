using Domain.Constructors;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Constructors
{
    public interface ICounstructorUnitOfWork
    {
        IConstructorRepository Constructors { get; set; }

        Task<int> Commit();
    }
}