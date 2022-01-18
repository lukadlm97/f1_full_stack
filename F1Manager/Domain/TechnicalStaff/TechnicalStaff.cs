using Domain.Base;
using Domain.Countries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TechnicalStaff
{
    [Table("TechnicalStaff")]
    public class TechnicalStaff : DeleteEntity<int>
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Title { get; set; }
        public string EducationDetails { get; set; }

        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
    }
}