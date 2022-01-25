using Domain.Base;
using Domain.Constructors;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ConstructorsPowerUnits
{
    public class ConstructorPowerUnit : EntityBase<int>
    {
        public int ConstructorId { get; set; }
        public int PowerUnitSupplierId { get; set; }
        public int YearEstaminate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsFabricConnection { get; set; }

        [ForeignKey(nameof(ConstructorId))]
        public virtual Constructor Constructor { get; set; }

        [ForeignKey(nameof(PowerUnitSupplierId))]
        public virtual PoweUnitSupplier.PowerUnitSupplier PowerUnitSupplier { get; set; }
    }
}