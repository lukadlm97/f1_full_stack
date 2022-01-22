using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.ConstructorRacingDetails
{
    public interface IConstructorRacingDetail : IRepository<ConstructorsRacingDetail>, IIncrementationOnChampionshipsAssets
    {
        Task<bool> CreateInitState(ConstructorsRacingDetail constructorsRacingDetail);

        Task<ConstructorsRacingDetail> GetById(int id);

        Task<bool> ChangeToApproprateConstructor(int constructorId, int newConstructorId);
    }
}