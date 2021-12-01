using Domain.ConstructorRacingDetails;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.ConstructorRacingDetails
{
    public class ConstructorRacingDetailsUoW : IConstructorRacingDetailsUnitOfWork
    {
        private readonly AppDbContext context;

        public ConstructorRacingDetailsUoW(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.context = dbContext;
            Constructors = new ConstructorRacingDetailRepository(dbContext, loggerFactory);
        }
        public IConstructorRacingDetail Constructors { set; get; }


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
