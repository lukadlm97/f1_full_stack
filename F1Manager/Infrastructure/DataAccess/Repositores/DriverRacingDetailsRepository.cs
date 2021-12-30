using Domain.DriversRacingDetails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class DriverRacingDetailsRepository : IDriverRacingRepository
    {
        public Task<bool> Delete(DriversRacingDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<DriversRacingDetails>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncrementConstructorChampionships(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncrementDriverChampionships(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncrementFastesLaps(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncrementPodiums(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncrementPolPositions(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncrementRaceVictories(int assetId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(DriversRacingDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(DriversRacingDetails entity)
        {
            throw new NotImplementedException();
        }
    }
}
