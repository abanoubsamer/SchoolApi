using Azure;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolWep.Infrustructure.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Infrustructure.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        
        public IGenericRepository<T>Repository<T>() where T :class;
        public Task<IDbContextTransaction> BeginTransactionAsync();
        public Task CommentAsync();
        public Task RollBackAsync();
        public Task<int> SaveChangesAsync();
      
    }
}
