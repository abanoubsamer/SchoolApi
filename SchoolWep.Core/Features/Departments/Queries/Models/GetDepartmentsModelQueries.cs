using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Departments.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentsModelQueries:IRequest<Response<List<GetDepartmentsQueriesResponse>>>
    {

    }
}
