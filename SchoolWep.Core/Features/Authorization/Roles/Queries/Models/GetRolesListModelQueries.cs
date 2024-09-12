using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authorization.Roles.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Roles.Queries.Models
{
    public class GetRolesListModelQueries:IRequest<Response<List<GetRolesListResponseQueries>>>
    {
    }
}
