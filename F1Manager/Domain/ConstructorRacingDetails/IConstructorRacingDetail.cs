using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.ConstructorRacingDetails
{
    public interface IConstructorRacingDetail : IRepository<ConstructorsRacingDetail>
    {
        Task<bool> CreateInitState(int constructorId);

        Task<bool> IncrementConstructorChampionships(int constructorId);

        Task<bool> IncrementDriverChampionships(int constructorId);

        Task<bool> IncrementRaceVictories(int constructorId);

        Task<bool> IncrementPodiums(int constructorId);

        Task<bool> IncrementPolPositions(int constructorId);

        Task<bool> IncrementFastesLaps(int constructorId);

        Task<bool> ChangeToApproprateConstructor(int constructorId, int newConstructorId);
    }
}