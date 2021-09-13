using FormulaManager.DAL.Entities.Origin;
using FormulaManager.DAL.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace FormulaManager.Persistance
{
    public partial class AppDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}