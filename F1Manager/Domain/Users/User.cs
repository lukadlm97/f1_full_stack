
using Domain.Base;
using Domain.Countries;
using Domain.Roles;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public bool IsVerified { get; set; }
        public int VerificationCode { get; set; }
        public DateTime VerificationDate { get; set; }

        public int RoleId { get; set; }
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }
    }
}