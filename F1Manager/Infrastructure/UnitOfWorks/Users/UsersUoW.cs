using Domain.Users;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Users
{
    public class UsersUoW : IUsersUoW
    {
        private readonly AppDbContext context;

        public UsersUoW(AppDbContext dbContext,IUserRepository userRepository,ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            Users = userRepository;
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