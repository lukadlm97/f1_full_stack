using FormulaManager.DAL.Entities.Repositories;
using FormulaManager.DAL.Entities.Users;
using FormulaManager.DAL.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormulaManager.BusinessLogic.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContextFactory _context;

        public UserRepository(AppDbContextFactory context)
        {
            _context = context;
        }

        public async Task<User> CreateNewUser(User user)
        {
            using (var db = _context.CreateDbContext())
            {
                var country = await db.Countries.SingleOrDefaultAsync(x => x.Id == user.Origin.Id);

                if (country == null)
                    return null;
                user.Origin = country;

                var createdUser = await db.AddAsync<User>(user);
                await db.SaveChangesAsync();

                return createdUser.Entity;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using (var db = _context.CreateDbContext())
            {
                var users = await db.Users.Include(x => x.Origin).ToListAsync();

                return users;
            }
        }
    }
}