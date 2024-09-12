using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Commend.Models
{
    public class ChangePasswordUserModelCommend:IRequest<Response<string>>
    {

        
        public string  Id { get; set; }
        public string  OldPassword { get; set; }
        public string  NewPassword { get; set; }
        public string  ComparePassword { get; set; }

    }
}
