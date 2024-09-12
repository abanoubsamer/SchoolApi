using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Claims.Queries.Models
{
    public class GetUserCliamsModelQueries:IRequest<Response<ResultMangeClaimUser>>
    {
        public string UserId { get; set; }

        public GetUserCliamsModelQueries(string userid)
        {
            UserId = userid;
        }

    }
}
