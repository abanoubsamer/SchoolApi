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
    public class DeleteRoleModelCommend : IRequest<Response<string>>
    {
        [Required]
        public string Name { get; set; }
        public DeleteRoleModelCommend(string name)
        {
            Name = name;
        }

    }
}
