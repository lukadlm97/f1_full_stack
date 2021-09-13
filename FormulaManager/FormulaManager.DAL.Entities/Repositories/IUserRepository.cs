using FormulaManager.DAL.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormulaManager.DAL.Entities.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateNewUser(User user);

        Task<IEnumerable<User>> GetAllUsers();
    }
}