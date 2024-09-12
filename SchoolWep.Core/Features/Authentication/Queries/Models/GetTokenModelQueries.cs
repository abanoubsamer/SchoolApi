using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authentication.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Queries.Models
{
    public class GetTokenModelQueries :IRequest<Response<LoginResponseQueries>>
    {
        public GetTokenModelQueries(string refreshtoken,string accesstoken)
        {
            RefreshToken = refreshtoken;
            AccessToken = accesstoken;
        }

        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }



    }
}
