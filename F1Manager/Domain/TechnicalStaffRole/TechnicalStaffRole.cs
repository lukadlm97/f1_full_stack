using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TechnicalStaffRole
{
    [Table("TechnicalStaffRoles")]
    public class TechnicalStaffRole : DeleteEntity<int>
    {
        public string RoleName { get; set; }
    }
}