using Domain.Users;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        private readonly RandomGenerator randomGenerator;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            this.randomGenerator = new RandomGenerator();
            this.logger = loggerFactory.CreateLogger<UserRepository>();
        }

        public Task<bool> Delete(User entity)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var forDelete = await context.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (forDelete == null)
                    return false;

                context.Users.Remove(forDelete);

                return true;
            }, "Delete User");
        }

        public Task<List<User>> GetAll()
        {
            return ExecuteInTryCatch<List<User>>(async () =>
            {
                return await context.Users.Where(x => !x.IsDeleted).Include(x => x.Role).ToListAsync();
            }, "GetAll User");
        }

        public Task<User> GetByID(long id)
        {
            return ExecuteInTryCatch<User>(async () =>
            {
                return await context.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
            }, "GetByID User");
        }

        public Task<List<User>> GetObjectByName(string value)
        {
            return ExecuteInTryCatch<List<User>>(async () =>
            {
                return await context.Set<User>()
                         .Where(x => x.Name.Contains(value) || x.Surname.Contains(value))
                         .ToListAsync();
            }, "GetObjectByName User");
        }

        public Task<User> GetObjectByUsername(string username)
        {
            return ExecuteInTryCatch<User>(async () =>
            {
                return await context.Set<User>()
                        .FirstOrDefaultAsync(x => x.UserName.Contains(username));
            }, "GetObjectByName User");
        }

        public Task<User> Login(string username, string password)
        {
            return ExecuteInTryCatch<User>(async () =>
            {
                var user = await context.Set<User>().Include(x => x.Role).FirstOrDefaultAsync(x => x.UserName == username && !x.IsDeleted);
                if (user == null)
                    return null;
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;
                return user;
            }, "Login User");
        }

        public Task<User> Register(User newUser, string password)
        {
            return ExecuteInTryCatch<User>(async () =>
            {
                if (await UserExists(newUser.UserName))
                {
                    return null;
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;
                newUser.Role = await context.Roles.FirstOrDefaultAsync(x => x.Id == newUser.RoleId);
                newUser.VerificationCode = randomGenerator.GenerateVerificationCode();
                newUser.Country = await context.Countries.FirstOrDefaultAsync(x => x.Id == newUser.CountryId);

                var entry = await context.Users.AddAsync(newUser);

                return entry.Entity;
            }, "Register User");
        }

        public Task<bool> Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExists(string username)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                if (await context.Set<User>().AnyAsync(x => x.UserName == username && !x.IsDeleted))
                    return true;
                return false;
            }, "UserExists User");
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

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Task<User> UpdateDetails(long id, User newUser, string password)
        {
            return ExecuteInTryCatch<User>(async () =>
            {
                var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (existingUser == null)
                {
                    return null;
                }

                existingUser.Name = newUser.Name;
                existingUser.Surname = newUser.Surname;
                existingUser.UserName = newUser.UserName;
                existingUser.Email = newUser.Email;
                existingUser.UpdatedBy = newUser.UpdatedBy;
                existingUser.UpdatedDate = newUser.UpdatedDate;

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                existingUser.PasswordHash = passwordHash;
                existingUser.PasswordSalt = passwordSalt;
                existingUser.Role = await context.Roles.FirstOrDefaultAsync(x => x.Id == newUser.RoleId);

                var entry = context.Users.Update(existingUser);

                return entry.Entity;
            }, "Register User");
        }

        public Task<bool> VerifyAccount(User user, string password)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (existingUser == null)
                {
                    return false;
                }

                if (!VerifyPasswordHash(password, existingUser.PasswordHash, existingUser.PasswordSalt))
                {
                    return false;
                }

                existingUser.IsVerified = true;
                existingUser.VerificationCode = 0;
                existingUser.VerificationDate = DateTime.Now;

                context.Users.Update(existingUser);

                return true;
            }, "UserExists User");
        }

        public Task<bool> AssignCountryToUser(int id, int countryId = default, string countryName = null)
        {
            return ExecuteInTryCatch<bool>(async () =>
            {
                var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                var existingCountry = countryId != default ? await context.Countries.FirstOrDefaultAsync(x => x.Id == countryId) :
                    await context.Countries.FirstOrDefaultAsync(x => x.Name.ToLower().Contains(countryName.ToLower()));

                if (existingUser == null || existingCountry == null)
                {
                    return false;
                }

                existingUser.Country = existingCountry;

                return true;
            }, "UserExists User");
        }

        private Task<T> ExecuteInTryCatch<T>(Func<Task<T>> databaseFunction, string errorMessage)
        {
            try
            {
                return databaseFunction();
            }
            catch (Exception e)
            {
                logger.LogError(e, errorMessage);
                throw;
            }
        }
    }
}