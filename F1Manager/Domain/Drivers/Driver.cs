using Domain.Base;
using Domain.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Drivers
{
    [Table("Drivers")]
    public partial class Driver : EntityBase<int>
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
        public bool IsActive { get; set; }
        public bool IsRetired { get; set; }
        public string PathToImage { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
        public ICollection<Contracts.Contract> Contracts { get; set; }

    }
}