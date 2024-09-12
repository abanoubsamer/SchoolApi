using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Students.Queries.Responed;
using SchoolWep.Core.Pagination;
using SchoolWep.Data.Enums.Oredring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginationQuery:IRequest<PaginationResult<GetStudentPaginationListResponse>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOredringEnum?  Oreder { get; set; }
        public OrederBy?  OrederBy { get; set; }
        public string?  Search { get; set; }

    }
}
