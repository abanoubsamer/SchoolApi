using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Roles.Commend.Models
{
    public class DeleteUserRoleModelCommend:IRequest<Response<string>>
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Roles { get; set; }


    }
}
