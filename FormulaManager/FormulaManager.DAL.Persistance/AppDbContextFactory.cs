using FormulaManager.DAL.Entities.Configurations;
using FormulaManager.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace FormulaManager.DAL.Persistance
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=FManager_Dev;Trusted_Connection=True;MultipleActiveResultSets=true";
        public AppDbContext CreateDbContext(string[] args = default)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseSqlServer(connectionString);

            return new AppDbContext(options.Options);
        }
    }
}