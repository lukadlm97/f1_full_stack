using Domain.Base;

namespace Domain.RacingChampionship
{
    public class RacingChampionship : EntityBase<int>
    {
        public string ChampionshipNameShort { get; set; }
        public string ChampionshipNameFull { get; set; }
        public string OrganisedBy { get; set; }
        public int FirstEntry { get; set; }
        public int LastEntry { get; set; }
        public int TotalCompetitions { get; set; }
    }
}