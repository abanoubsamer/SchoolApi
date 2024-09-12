using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authentication.Commend.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Commend.Models
{
    public class RegisterModelCommend:IRequest<Response<string>>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName => FirstName + LastName;
        
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string ComparePassword { get; set; }

    }
}
