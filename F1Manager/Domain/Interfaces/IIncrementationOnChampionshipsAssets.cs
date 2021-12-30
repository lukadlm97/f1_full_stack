using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IIncrementationOnChampionshipsAssets
    {
        Task<bool> IncrementConstructorChampionships(int assetId);

        Task<bool> IncrementDriverChampionships(int assetId);

        Task<bool> IncrementRaceVictories(int assetId);

        Task<bool> IncrementPodiums(int assetId);

        Task<bool> IncrementPolPositions(int assetId);

        Task<bool> IncrementFastesLaps(int assetId);
    }
}
