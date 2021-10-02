using System.Collections.Generic;

namespace WebApi.Models
{
    public class AccountsView
    {
        public IEnumerable<SingleAcountView> Accounts { get; set; }
    }

    public class SingleAcountView
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
    }
}