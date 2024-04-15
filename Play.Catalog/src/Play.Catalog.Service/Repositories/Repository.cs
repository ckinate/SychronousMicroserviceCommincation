using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Play.Catalog.Service.Data;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{

    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {

                _context.Entry(entity).State = EntityState.Modified;
                // Save changes to the database
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }


        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                // Check if the entity exists
                if (entity == null)
                {
                    throw new ArgumentException($"Entity with ID {id} not found.", nameof(id));
                }
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException)
            {

            }
        }
    }
}