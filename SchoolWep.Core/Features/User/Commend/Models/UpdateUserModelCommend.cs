using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Commend.Models
{
    public class UpdateUserModelCommend:IRequest<Response<string>>
    {
        public string Id  { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string UserName => FirstName + LastName;
    }
}
