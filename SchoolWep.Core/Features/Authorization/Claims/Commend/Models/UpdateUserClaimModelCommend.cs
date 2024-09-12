using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Claims.Commend.Models
{
    public class UpdateUserClaimModelCommend : IRequest<Response<string>>
    {
        public string UserId { get; set; }

        public UserClaims UserClaims { get; set; } = new();
 
    }

   
}
