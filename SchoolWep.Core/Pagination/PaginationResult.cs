using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolWep.Core.Pagination
{
    public class PaginationResult<T>
    {
        public  List<T> Data { get; set; }

        public PaginationResult(List<T> data)
        {
            Data = data;
        }

        internal PaginationResult(
            bool succeeded,
            List<T> data = default,
            List<string>masseges=null,
            int count =0,
            int page=1,
            int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int) Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        // Total Page 100/10 => 10
        // 
        public static PaginationResult<T> Success(List<T> Data , int count ,int page, int pageszie)
        {
            return new PaginationResult<T>(true, Data, null, count, page, pageszie);
        }


        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public object Meta { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public List<string> Messages { get; set; } = new();

        public bool Succeeded { get; set; }


    }
}
