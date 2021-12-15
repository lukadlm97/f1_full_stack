using Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Contracts
{
    [Table("Contract")]
    public class Contract : AuditEntity<int>
    {
        public double EstaminateValue { get; set; }
        public int EstaminateYears { get; set; }
        public DateTime EndOfContactDate { get; set; }
        public DateTime StartOfContactDate { get; set; }

        [ForeignKey(nameof(ConstructorId))]
        public virtual Constructors.Constructor Constructor { get; set; }
        public int ConstructorId { get; set; }

        [ForeignKey(nameof(DriverId))]
        public virtual Drivers.Driver Driver { get; set; }
        public int DriverId { get; set; }

        [ForeignKey(nameof(DriverRolesId))]
        public virtual DriverRoles.DriverRoles DriverRoles { get; set; }
        public int DriverRolesId { get; set; }
    }
}