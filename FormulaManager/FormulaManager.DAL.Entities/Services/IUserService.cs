using FormulaManager.DAL.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormulaManager.DAL.Entities.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllUsers();

        public Task<User> RegisterUser(User user);

        public Task<bool> ConfirmAccount(string username);
    }
}