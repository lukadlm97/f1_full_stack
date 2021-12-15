using Domain.Base;
using Domain.Countries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Constructors
{
    [Table("Constructors")]
    public class Constructor : AuditEntity<int>
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

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }


        public ICollection<Contracts.Contract> Contracts { get; set; }
    }
}