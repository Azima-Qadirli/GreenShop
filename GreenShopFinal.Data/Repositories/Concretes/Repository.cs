using GreenShopFinal.Core.Entities.Base;
using GreenShopFinal.Core.Repositories.Abstractions;
using GreenShopFinal.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreenShopFinal.Data.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly GreenShopFinalDbContext _dbContext;

        public Repository(GreenShopFinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }



        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }
    }
}
