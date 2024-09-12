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
    public class UpdateRoleClaimModelCommend : IRequest<Response<string>>
    {
        public List<RoleClaims> RoleClaims { get; set; } = new();
    }
}
