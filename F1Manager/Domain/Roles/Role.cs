using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Roles
{
    [Table("Roles")]
    public partial class Role:DeleteEntity<int>
    {
        public Role()
        {

        }
        public string RoleName { get; set; }

    }
}
