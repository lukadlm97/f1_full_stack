using Domain.Base;
using Domain.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Users
{
    [Table("Users")]
    public partial class User : AuditEntity<int>
    {
        public User()
        {
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

    }
}
