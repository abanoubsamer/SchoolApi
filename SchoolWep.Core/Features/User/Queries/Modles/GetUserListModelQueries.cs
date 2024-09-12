using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.User.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Queries.Modles
{
    public class GetUserListModelQueries:IRequest<Response<List<GetUserListResponseQueries>>>
    {


    }
}
