using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetObjectByName(string value);
        Task<User> GetByID(long id);
        Task<bool> UserExists(string username);
        Task<User> Register(User newUser, string password);
        Task<User> Login(string username, string password);
        Task<User> GetObjectByUsername(string username);
        Task<User> UpdateDetails(long id, User newUser,string password);
        Task<bool> VerifyAccount(User user, string password);
    }
}