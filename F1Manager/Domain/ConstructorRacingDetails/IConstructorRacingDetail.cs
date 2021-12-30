using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.ConstructorRacingDetails
{
    public interface IConstructorRacingDetail : IRepository<ConstructorsRacingDetail>, IIncrementationOnChampionshipsAssets
    {
        Task<bool> CreateInitState(int constructorId);

        Task<bool> ChangeToApproprateConstructor(int constructorId, int newConstructorId);
    }
}