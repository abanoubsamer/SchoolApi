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
    public class UpdateRolesModelCommend:IRequest<Response<string>>
    {
        [Required]
        public string OldName { get; set; }

        [Required]
        public string Name { get; set; }

        public string NormalizedName => Name.ToUpper();

        public string ConcurrencyStamp => Guid.NewGuid().ToString();

    }
}
