﻿using AltYapi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AltYapi.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //Temel crud operasyonları Yeterli olmadığı yerlerde dbcontext e erişebilmek için protected yaptık.
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           

            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
           

            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //Birden fazla PK olabilir FindAsync ona göre değer alabiliyor ihtiyaç halinde yenisi yazılacak.
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
          
            _dbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
           
            _dbSet.UpdateRange(entities);
        }


        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}