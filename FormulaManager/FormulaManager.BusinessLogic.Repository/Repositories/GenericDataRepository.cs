using FormulaManager.DAL.Entities.Base;
using FormulaManager.DAL.Entities.Repositories;
using FormulaManager.DAL.Persistance;
using FormulaManager.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormulaManager.BusinessLogic.Repository.Repositories
{
    public class GenericDataRepository<T> : IDataService<T> where T : EntityBase
    {
        private readonly AppDbContextFactory _context;
        private readonly NonQueryDataRepository<T> _nonQueryDataRepository;

        public GenericDataRepository(AppDbContextFactory appDbContextFactory)
        {
            _context = appDbContextFactory;
            _nonQueryDataRepository = new NonQueryDataRepository<T>(appDbContextFactory);
        }

        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataRepository.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataRepository.Delete(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (AppDbContext context = _context.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();

                return entities;
            }
        }

        public async Task<T> GetById(int id)
        {
            using (AppDbContext context = _context.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);

                return entity;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            return await _nonQueryDataRepository.Update(id, entity);
        }
    }
}