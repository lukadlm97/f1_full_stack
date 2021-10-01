using Domain.Users;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Users
{
    public class UsersUoW : IUsersUoW
    {
        private readonly AppDbContext context;

        public UsersUoW(AppDbContext dbContext)
        {
            this.context = dbContext;
            Users = new UserRepository(dbContext);
        }

        public IUserRepository Users { get; set; }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        Task<int> IUsersUoW.Commit()
        {
            return context.SaveChangesAsync();
        }
    }
}