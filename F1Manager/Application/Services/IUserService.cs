using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<Domain.Users.User> GetIndentity(string username, string password);
        Task<int> Register(Domain.Users.User user, string password);
    }
}
