using Domain.TechnicalStaffRole;
using Infrastructure.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.TechnicalStuffRole
{
    public class TechnicalStaffRoleUoW : ITechnicalStaffRoleUnitOfWork
    {
        private readonly AppDbContext context;

        public TechnicalStaffRoleUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            TechnicalStaffRole = new Infrastructure.DataAccess.Repositores.TechnicalStaffRoleRepository(dbContext, loggerFactory);
        }


        public ITechnicalStaffRoleRepository TechnicalStaffRole { get; set; }

        public async Task<int> Commit()
        {
            return await context.SaveChangesAsync();
        }
    }
}
