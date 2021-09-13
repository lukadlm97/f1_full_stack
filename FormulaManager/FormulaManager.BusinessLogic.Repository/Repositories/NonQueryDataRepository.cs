using FormulaManager.DAL.Entities.Base;
using FormulaManager.DAL.Persistance;
using FormulaManager.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace FormulaManager.BusinessLogic.Repository.Repositories
{
    public class NonQueryDataRepository<T> where T : EntityBase
    {
        private readonly AppDbContextFactory _context;

        public NonQueryDataRepository(AppDbContextFactory context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity)
        {
            using (AppDbContext context = _context.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (AppDbContext context = _context.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);

                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (AppDbContext context = _context.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}