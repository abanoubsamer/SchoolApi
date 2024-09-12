using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Pagination.Extensions
{
    public static class QueryableExtension
    {
        public static async Task<PaginationResult<T>> ToPaginationListAsync<T>(
            this IQueryable<T> source ,
            int pageNumber,
            int pagesize)
            where T : class
        {
            if(source==null)
            {
                throw new Exception("Empty");
                
            }
            // dufelt Value
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            // dufelt Value
            pagesize = pagesize == 0 ? 10 : pagesize;
             
            // total count
            int count = await source.AsNoTracking().CountAsync();

            // hna lw ale students table Empty
            if (count == 0) return PaginationResult<T>.Success(new List<T>(), count, pagesize, pagesize);

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
   
            var items = await source.Skip((pageNumber - 1) * pagesize).Take(pagesize).AsNoTracking().ToListAsync();

            return PaginationResult<T>.Success(items, count, pageNumber, pagesize);

        }
      
    }
}
