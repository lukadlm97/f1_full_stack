using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Season
{
    public class Season : EntityBase<int>
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public int RacingChampionshipId { get; set; }

        [ForeignKey(nameof(RacingChampionshipId))]
        public virtual RacingChampionship.RacingChampionship RacingChampionship { get; set; }
    }
}