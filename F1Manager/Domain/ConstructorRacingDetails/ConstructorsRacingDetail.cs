using Domain.Base;
using Domain.Constructors;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ConstructorRacingDetails
{
    [Table("ConstructorsRacingDetails")]
    public class ConstructorsRacingDetail : EntityBase<int>
    {
        public int ConstructorChampionships { get; set; }
        public int DriverChampionships { get; set; }
        public int RaceVictories { get; set; }
        public int Podiums { get; set; }
        public int PolPositions { get; set; }
        public int FastesLaps { get; set; }
        public int ConstructorId { get; set; }

        [ForeignKey(nameof(ConstructorId))]
        public virtual Constructor Constructor { get; set; }
    }
}