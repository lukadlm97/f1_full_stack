using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext dbContext)
        {
            this.context = dbContext;
        }

        public Task<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                return await context.Users.Where(x => !x.IsDeleted).Include(x => x.Role).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<User> GetByID(long id)
        {
            try
            {
                return await context.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                return null;
            }
        }

        public async Task<List<User>> GetObjectByName(string value)
        {
            try
            {
                return await context.Set<User>()
                        .Where(x => x.Name.Contains(value) || x.Surname.Contains(value))
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                return null;
            }
        }

        public async Task<User> GetObjectByUsername(string username)
        {
            try
            {
                return await context.Set<User>()
                        .FirstOrDefaultAsync(x => x.UserName.Contains(username));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                return null;
            }
        }

        public async Task<User> Login(string username, string password)
        {
            try
            {
                var user = await context.Set<User>().Include(x=>x.Role).FirstOrDefaultAsync(x => x.UserName == username && !x.IsDeleted);
                if (user == null)
                    return null;
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                return null;
            }
        }

        public async Task<User> Register(User newUser, string password)
        {
            try
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

                var entry = await context.Users.AddAsync(newUser);

                return entry.Entity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                return null;
            }
        }

        public Task<bool> Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public  Task<bool> Update(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                if (await context.Set<User>().AnyAsync(x => x.UserName == username && !x.IsDeleted))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                return false;
            }
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

        public async Task<User> UpdateDetails(long id, User newUser,string password)
        {
            try
            {
                var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (existingUser==null)
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

                var entry =  context.Users.Update(existingUser);

                return entry.Entity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>> " + ex.Message);
                return null;
            }
        }
    }
}