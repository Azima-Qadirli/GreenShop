using GreenShopFinal.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Core.Repositories.Abstractions
{
    public interface IRepository<T>where T : BaseEntity
    {
        public Task AddAsync(T entity);
        public IQueryable<T> GetAll(Expression<Func<T,bool>>expression);
        public Task<T>GetAsync(Expression<Func<T,bool>>expression);
        public Task<T> GetByIdAsync(Guid id);
        public void Update (T entity);
        public void Delete (T entity);
        public Task<int> SaveAsync();
        public int Save();
    }
}
