using FormulaManager.DAL.Entities.Base;
using FormulaManager.DAL.Entities.Origin;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormulaManager.DAL.Entities.Users
{
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } = default;
        public string Facebook { get; set; } = default;
        public string Instagram { get; set; } = default;
        public DateTime DateOfBirth { get; set; }
        public bool IsConfirmed { get; set; } = default;

        [ForeignKey("CountryId")]
        public Country Origin { get; set; }
    }
}