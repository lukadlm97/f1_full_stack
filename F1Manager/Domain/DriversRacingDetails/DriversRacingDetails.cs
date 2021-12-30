using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DriversRacingDetails
{
    public class DriversRacingDetails : EntityBase<int>
    {
        public int ConstructorChampionships { get; set; }
        public int DriverChampionships { get; set; }
        public int RaceVictories { get; set; }
        public int Podiums { get; set; }
        public int PolPositions { get; set; }
        public int FastesLaps { get; set; }
        public int CompetitionId { get; set; }
        public int DriverId { get; set; }

        [ForeignKey(nameof(CompetitionId))]
        public virtual RacingChampionship.RacingChampionship RacingChampionship { get; set; }

        [ForeignKey(nameof(DriverId))]
        public virtual Drivers.Driver Driver { get; set; }
    }
}