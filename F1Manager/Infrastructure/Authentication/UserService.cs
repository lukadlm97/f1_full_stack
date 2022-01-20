using Domain.Users;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class UserService : Application.Services.IUserService
    {
        private readonly UnitOfWorks.Users.IUsersUoW userUoW;

        public UserService(UnitOfWorks.Users.IUsersUoW userUoW)
        {
            this.userUoW = userUoW;
        }

        public async Task<User> GetIndentity(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            // get the user to verifty
            var userToVerify = await this.userUoW.Users.GetObjectByUsername(username);

            if (userToVerify == null) return null;

            if (!VerifyPasswordHash(password, userToVerify.PasswordHash, userToVerify.PasswordSalt))
            {
                return null;
            }

            return userToVerify;
        }

        public async Task<int> Register(User user, string password)
        {
            var createdUser = await this.userUoW.Users.Register(user, password);
            if (!createdUser)
            {
                return default;
            }
            var countOfChanges = await this.userUoW.Commit();

            if (countOfChanges != 0)
            {
                return user.Id;
            }
            return default;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}