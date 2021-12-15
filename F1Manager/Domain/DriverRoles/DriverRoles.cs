using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.DriverRoles
{
    [Table("DriverRoles")]
    public partial class DriverRoles : EntityBase<int>
    {
        public string RoleName { get; set; }
        public bool IsValid { get; set; }
    }
}
