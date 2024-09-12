using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolWep.Infrustructure.Data;
using System.Linq.Expressions;


namespace SchoolWep.Infrustructure.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        #region Fiald
        private readonly DbSet<T> _DbSet;
        private readonly AppDbContext _db;
        #endregion

        #region Constructor
        public GenericRepository(AppDbContext db)
        {
            _db = db;
            _DbSet = _db.Set<T>();
        }
        #endregion


        #region Implantation
        public async Task AddAsync(T entity)
        {
           
            await _DbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
          return await _db.Database.BeginTransactionAsync();
        }

        public async Task CommintAsync()
        {
            await _db.Database.CommitTransactionAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _DbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(ICollection<T> entitys)
        {
            _DbSet.RemoveRange(entitys);
            await _db.SaveChangesAsync();
        }

        public async Task<List<T>> FindMoreAsNoTrackingAsync(Expression<Func<T, bool>> Match)
        {
            return await _DbSet.AsNoTracking().Where(Match).ToListAsync();
        }

        public async Task<T> FindOneAsNoTrackingAsync(Expression<Func<T, bool>> Match)
        {
            return await _DbSet.AsNoTracking().FirstOrDefaultAsync(Match);  
        }
        public async Task<List<T>> FindMoreAsync(Expression<Func<T, bool>> Match)
        {
            return await _DbSet.Where(Match).ToListAsync();
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> Match)
        {
            return await _DbSet.FirstOrDefaultAsync(Match);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _DbSet.AsNoTracking().ToListAsync();
        }

        public  IQueryable<T> GetQueryable()
        {
            return  _DbSet.AsQueryable();
        }

        public IQueryable<T> GetTableAsNotTraking()
        {
          return _DbSet.AsNoTracking().AsQueryable();
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> Match)
        {
            return await _DbSet.AnyAsync(Match);
        }

        public async Task RolleBackAsync()
        {
            await _db.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task UpdataRangeAsync(ICollection<T> entitys)
        {
            _DbSet.UpdateRange(entitys);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _DbSet.Update(entity);
            await _db.SaveChangesAsync();
        }



        #endregion


    }
}
