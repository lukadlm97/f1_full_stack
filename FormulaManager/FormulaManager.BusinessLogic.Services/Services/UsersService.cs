using FormulaManager.DAL.Entities.Repositories;
using FormulaManager.DAL.Entities.Services;
using FormulaManager.DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormulaManager.BusinessLogic.Services.Services
{
    public class UsersService : IUserService
    {
        private readonly IDataService<User> _genericUsersRepository;
        private readonly IUserRepository _userRepository;

        public UsersService(IDataService<User> genericUsersRepository, IUserRepository userRepository)
        {
            _genericUsersRepository = genericUsersRepository;
            _userRepository = userRepository;
        }

        public Task<bool> ConfirmAccount(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return (await _userRepository.GetAllUsers());
        }

        public async Task<User> RegisterUser(User user)
        {
            return (await _userRepository.CreateNewUser(user));
        }
    }
}