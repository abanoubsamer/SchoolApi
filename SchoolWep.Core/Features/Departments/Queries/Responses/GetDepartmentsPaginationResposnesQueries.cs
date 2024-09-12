using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Departments.Queries.Responses
{
    public class GetDepartmentsPaginationResposnesQueries: GetDepartmentsQueriesResponse
    {

        public GetDepartmentsPaginationResposnesQueries(int id,string FullName,int capsity, string? manager)
        {
            Id = id;
            Name = FullName;
            Capsity = capsity;
            Manager = manager;
        }

    }
}
