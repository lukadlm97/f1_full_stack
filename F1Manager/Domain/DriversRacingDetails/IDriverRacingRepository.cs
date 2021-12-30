using Domain.Interfaces;

namespace Domain.DriversRacingDetails
{
    public interface IDriverRacingRepository : IRepository<DriversRacingDetails>, IIncrementationOnChampionshipsAssets
    {
    }
}