using Domain.Base;
using Domain.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.PoweUnitSupplier
{
    public class PowerUnitSupplier:EntityBase<int>
    {
        public string SupplierName { get; set; }
        public bool IsActive { get; set; } = true;
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
    }
}
