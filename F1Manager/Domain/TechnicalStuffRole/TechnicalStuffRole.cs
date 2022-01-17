using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TechnicalStuffRole
{
    [Table("TechnicalStuffRoles")]
    public class TechnicalStuffRole : DeleteEntity<int>
    {
        public string RoleName { get; set; }
    }
}