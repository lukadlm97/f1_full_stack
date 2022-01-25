using Infrastructure.DataAccess;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.ConstructorsPowerUnit
{
    public class ConstructorsPowerUnitUoW : IConstructorsPowerUnit
    {
        private readonly AppDbContext context;

        public ConstructorsPowerUnitUoW(AppDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            ConstructorsPowerUnit =
                new Infrastructure.DataAccess.Repositores.ConstructorsPowerUnitRepository(this.context, loggerFactory);
        }

        public Domain.ConstructorsPowerUnits.IConstructorsPowerUnit ConstructorsPowerUnit { get; set; }

        public Task<int> Commit()
        {
            return context.SaveChangesAsync();
        }
    }
}