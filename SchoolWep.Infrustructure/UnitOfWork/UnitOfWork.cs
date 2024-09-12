using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Infrustructure.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Infrustructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly Dictionary<Type, object> _Repository;
        private readonly AppDbContext _db;
        private  IDbContextTransaction _dbTransaction;
        private bool _disposed = false;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            _Repository = new Dictionary<Type, object>();
            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    _db.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_Repository.ContainsKey(typeof(T)))
            {
                var repository=_Repository[typeof(T)] as GenericRepository<T>;
                if (repository != null)
                {
                    return repository;
                }
            }
            var _Repo = new GenericRepository<T>(_db);
            _Repository.Add(typeof(T), _Repo);
            return _Repo;

        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_dbTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }
            _dbTransaction = await _db.Database.BeginTransactionAsync();
            return _dbTransaction;
        }

        public async Task CommentAsync()
        {
            if (_dbTransaction == null)
            {
                throw new InvalidOperationException("No transaction is in progress.");
            }
            try
            {
                await _db.SaveChangesAsync();
                await _dbTransaction.CommitAsync();
            }
            catch
            {
                await RollBackAsync();
                throw;
            }
        }

        public async Task RollBackAsync()
        {
            if (_dbTransaction == null)
            {
                throw new InvalidOperationException("No transaction is in progress.");
            }
            await _dbTransaction.RollbackAsync();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

    
    }
}
