using Domain.ConstructorRacingDetails;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.ConstructorRacingDetails
{
    public interface IConstructorRacingDetailsUnitOfWork
    {
        IConstructorRacingDetail Constructors { get; set; }

        Task<int> Commit();
    }
}