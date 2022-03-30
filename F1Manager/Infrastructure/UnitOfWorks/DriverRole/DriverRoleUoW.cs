using Domain.DriverRoles;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.DriverRole
{
    public class DriverRoleUoW : IDriverRoleUnitOfWork
    {
        private readonly AppDbContext context;

        public DriverRoleUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            DriverRolesRepository = new DriverRolesRepository(dbContext, loggerFactory);
        }

        public IDriverRolesRepository DriverRolesRepository { get; set; }

        public Task<int> Commit()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}