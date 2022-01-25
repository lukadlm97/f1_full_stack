using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.PoweUnitSupplier
{
    public class PowerUnitSupplier:EntityBase<int>
    {
        public string SupplierName { get; set; }
        public bool IsActive { get; set; }
    }
}
