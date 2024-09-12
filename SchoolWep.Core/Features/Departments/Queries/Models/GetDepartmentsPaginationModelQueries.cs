using MediatR;
using SchoolWep.Core.Features.Departments.Queries.Responses;
using SchoolWep.Core.Pagination;
using SchoolWep.Data.Enums.Oredring;


namespace SchoolWep.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentsPaginationModelQueries:IRequest<PaginationResult<GetDepartmentsPaginationResposnesQueries>>
    {

        public  int NumberPage { get; set; }
        public  int SizePage { get; set; }
        public  DepartmentOrederingEnum? Oreder { get; set; }
        public  OrederBy? OrederBy { get; set; }
        public string? Search { get; set; }

    }
}
