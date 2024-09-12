using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authorization.Roles.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Roles.Commend.Models
{
    public class AddUserRoleModelCommend: IRequest<Response<string>>
    {

        public string UserId { get; set; }

        public List<string> Roles { get; set; }

    }
 
}
