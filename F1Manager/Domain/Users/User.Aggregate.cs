using Domain.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Users
{
    public partial class User
    {
        public User(string username,string email,Role role)
        {
            UserName = username;
            Email = email;
            Role = role;
        }

        public bool ValidOnAdd()
        {
            return
                // Validate userName
                !string.IsNullOrEmpty(UserName)
                // Make sure email not null and correct email format
                && !string.IsNullOrEmpty(Email)
                && new EmailAddressAttribute().IsValid(Email)
                // Make sure department not null
                && (
                    Role != null
                    || RoleId != 0
                );
        }
    }
}
