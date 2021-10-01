using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositores
{
    public class Repository<T,TContext>:IRepository<T>
        where T:class
        where TContext:AppDbContext
    {

        protected readonly AppDbContext context;
        private DbSet<T> _entities;

        public Repository(AppDbContext context)
        {
            this.context = context;
            _entities = context.Set<T>();
        }

        public async virtual Task<bool> Delete(T entity)
        {
            try
            {
                _entities.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + e.Message);
                return false;
            }
        }

        public async virtual Task<List<T>> GetAll()
        {
            try
            {
                return await _entities.ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        public async virtual Task<bool> Insert(T entity)
        {
            try
            {
                await _entities.AddAsync(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>> " + ex.Message);
                return false;
            }
        }

        public async virtual Task<bool> Update(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>> " + ex.Message);
                return false;
            }
        }
    }
}
