using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.ConstructorsStaffContracts
{
    public class ConstructorsStaffContracts: DeleteEntity<int>
    {
        public DateTime DateOfSign { get; set; }
        public DateTime? DateOfEnd { get; set; }
        

        [ForeignKey(nameof(ConstructorId))]
        public virtual Constructors.Constructor Constructor { get; set; }
        public int ConstructorId { get; set; }

        [ForeignKey(nameof(TechnicalStaffId))]
        public virtual TechnicalStaff.TechnicalStaff TechnicalStaff { get; set; }
        public int TechnicalStaffId { get; set; }

        [ForeignKey(nameof(TechnicalStaffRoleId))]
        public virtual TechnicalStaffRole.TechnicalStaffRole TechnicalStaffRole { get; set; }
        public int TechnicalStaffRoleId { get; set; }

    }
}
