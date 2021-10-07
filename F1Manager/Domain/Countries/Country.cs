using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Countries
{
    [Table("Countries")]
    public partial class Country: EntityBase<int>
    {
        public int KeggleId { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public decimal NominalGDP { get; set; }
        public decimal GDPPerCapita { get; set; }
        public decimal ShareIfWorldGDP { get; set; }
        public string Code { get; set; }
    }
}
