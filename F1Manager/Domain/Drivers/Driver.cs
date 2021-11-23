using Domain.Base;
using Domain.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Drivers
{
    [Table("Drivers")]
    public partial class Driver:EntityBase<int>
    {
        public int KeggleId { get; set; }
        public string DriverRef { get; set; }
        public int? Number { get; set; }
        public string Code { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string WikiUrl { get; set; }
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
    }
}
