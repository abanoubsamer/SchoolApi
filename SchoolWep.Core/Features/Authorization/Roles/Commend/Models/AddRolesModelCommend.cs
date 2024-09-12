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
    public class AddRolesModelCommend:IRequest<Response<string>>
    {
        public string Id => Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        public string NormalizedName => Name.ToUpper();

        public string ConcurrencyStamp => Guid.NewGuid().ToString();

        
    }
}
