namespace WebApi.DTOs.ConstructorsResult
{
    public class ConstructorRacingResultDto
    {
        public int Id { get; set; }
        public int ConstructorChampionships { get; set; }
        public int DriverChampionships { get; set; }
        public int RaceVictories { get; set; }
        public int Podiums { get; set; }
        public int PolPositions { get; set; }
        public int FastesLaps { get; set; }
        public int ConstructorId { get; set; }
        public int CompetitionId { get; set; }
    }
}
