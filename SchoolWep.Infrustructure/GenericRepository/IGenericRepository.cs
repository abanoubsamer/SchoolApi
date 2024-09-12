using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace SchoolWep.Infrustructure.GenericRepository
{
    public interface IGenericRepository<T> where T: class
    {

        public Task<List<T>> GetAllAsync();
        public Task<T> FindOneAsync(Expression<Func<T, bool>> Match);
        public Task<List<T>> FindMoreAsync(Expression<Func<T, bool>> Match);
        public Task<List<T>> FindMoreAsNoTrackingAsync(Expression<Func<T, bool>> Match);
        public  Task<T> FindOneAsNoTrackingAsync(Expression<Func<T, bool>> Match);
        public IQueryable<T> GetQueryable();
       
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<bool> IsExistAsync(Expression<Func<T, bool>> Match);
        public  IQueryable<T> GetTableAsNotTraking();
        public  Task DeleteRangeAsync(ICollection<T> entitys);
        public  Task UpdataRangeAsync(ICollection<T> entitys);
        public  Task<IDbContextTransaction> BeginTransactionAsync();
        public  Task CommintAsync();
        public  Task<int> SaveChangesAsync();
        public  Task RolleBackAsync();

    }
}
