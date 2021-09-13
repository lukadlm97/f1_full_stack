using System;

namespace FormulaManager.UsersWebApi.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } = default;
        public string Facebook { get; set; } = default;
        public string Instagram { get; set; } = default;
        public DateTime DateOfBirth { get; set; }

        public CountryDto Origin { get; set; }
    }
}