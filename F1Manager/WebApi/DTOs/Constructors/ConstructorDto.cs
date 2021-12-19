namespace WebApi.DTOs.Constructors
{
    public class ConstructorDto
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Base { get; set; }
        public string ChiefTechnicalOfficer { get; set; }
        public string TechnicalDirector { get; set; }
        public string Website { get; set; }
        public string FirstEntry { get; set; }
        public string LastEntry { get; set; }

        public int CountryId { get; set; }
    }
}
